using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class DataPath : IDataPath
    {
        private bool _hasChanges;
        private bool _canExecute;
        private List<INode> _nodes;

        private readonly IInputSocket _flowInput;
        private readonly Dictionary<Guid, int> _nodesDepth;

        public DataPath(IInputSocket flowInput)
        {
            _flowInput = flowInput;

            _nodes = new List<INode>();
            _nodesDepth = new Dictionary<Guid, int>();
        }

        public void Construct()
        {
            SubscribeToNodeInputs(_flowInput.Connection.Output.Node, 0);
            SortNodes();
            ValidateNodes();
        }

        private void SortNodes()
        {
            if (_hasChanges)
            {
                _hasChanges = false;
                _nodes = _nodes.OrderByDescending(node => _nodesDepth[node.Guid]).ToList();
            }
        }

        private void ValidateNodes()
        {
            var readyNodesCount = 0;
            for (var i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].AllInputsReady == false)
                {
                    break;
                }

                readyNodesCount++;
            }

            _canExecute = readyNodesCount == _nodes.Count;
        }

        public void Execute()
        {
            if (_canExecute)
            {
                ExecutePath();
            }
        }
        
        public void Reset()
        {
            UnsubscribeFromNodeInputs(_flowInput.Connection.Output.Node);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ExecutePath()
        {
            // TODO: Skip if no changes.
            
            for (var i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].Execute();
            }
        }
        
        private void SubscribeToNodeInputs(INode node, int depth)
        {
            if (IsNodeAdded(node.Guid))
            {
                return;
            }
            
            _nodes.Add(node);
            _hasChanges = true;
            _nodesDepth.Add(node.Guid, depth);

            if (node.HasInputs == false)
            {
                return;
            }
            
            for (var i = 0; i < node.Inputs.Length; i++)
            {
                var input = node.Inputs[i];
                if (input.HasConnections)
                {
                    SubscribeToNodeInputs(input.Connection.Output.Node, depth + 1);
                }
                
                input.Connected += OnDataNodeInputConnected;
                input.Disconnecting += OnDataNodeInputDisconnecting;
            }
        }

        private bool IsNodeAdded(Guid guid)
        {
            return _nodesDepth.ContainsKey(guid);
        }

        private void UnsubscribeFromNodeInputs(INode node)
        {
            _hasChanges = true;
            _nodes.Remove(node);
            _nodesDepth.Remove(node.Guid);

            if (node.HasInputs == false)
            {
                return;
            }
            
            for (var i = 0; i < node.Inputs.Length; i++)
            {
                var input = node.Inputs[i];
                if (input.HasConnections)
                {
                    UnsubscribeFromNodeInputs(input.Connection.Output.Node);
                }

                input.Connected -= OnDataNodeInputConnected;
                input.Disconnecting -= OnDataNodeInputDisconnecting;
            }
        }
        
        private void OnDataNodeInputConnected(object sender, IConnection connection)
        {
            SubscribeToNodeInputs(connection.Output.Node, _nodesDepth[connection.Input.Node.Guid] + 1);
            SortNodes();
            ValidateNodes();
        }

        private void OnDataNodeInputDisconnecting(object sender, IConnection connection)
        {
            UnsubscribeFromNodeInputs(connection.Output.Node);
        }
    }
}