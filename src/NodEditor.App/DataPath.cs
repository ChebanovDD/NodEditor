using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class DataPath
    {
        private bool _hasChanges;
        private int _readyNodesCount;
        private List<INode> _nodes = new();

        private readonly IInputSocket _flowInput;
        private readonly Dictionary<Guid, int> _nodesDepth = new();

        public DataPath(IInputSocket flowInput)
        {
            _flowInput = flowInput;
        }

        public void Construct()
        {
            ObserveNodeInputs(_flowInput.Connection.Output.Node, 0);
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
            _readyNodesCount = 0;
            for (var i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].AllInputsReady == false)
                {
                    break;
                }

                _readyNodesCount++;
            }
        }

        public void Execute()
        {
            if (_readyNodesCount == _nodes.Count)
            {
                ExecutePath();
            }
        }
        
        public void Reset()
        {
            OverlookNodeInputs(_flowInput.Connection.Output.Node);
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
        
        private void ObserveNodeInputs(INode node, int depth)
        {
            if (_nodesDepth.ContainsKey(node.Guid))
            {
                return;
            }
            
            _nodesDepth.Add(node.Guid, depth);

            if (node.HasInputs == false)
            {
                AddNode(node);
                return;
            }
            
            for (var i = 0; i < node.Inputs.Count; i++)
            {
                var input = node.Inputs[i];
                if (input.HasConnections)
                {
                    ObserveNodeInputs(input.Connection.Output.Node, depth + 1);
                }
                
                input.Connected += OnDataNodeInputConnected;
                input.Disconnecting += OnDataNodeInputDisconnecting;
            }

            AddNode(node);
        }
        
        private void OverlookNodeInputs(INode node)
        {
            _nodesDepth.Remove(node.Guid);

            if (node.HasInputs == false)
            {
                RemoveNode(node);
                return;
            }
            
            for (var i = 0; i < node.Inputs.Count; i++)
            {
                var input = node.Inputs[i];
                if (input.HasConnections)
                {
                    OverlookNodeInputs(input.Connection.Output.Node);
                }

                input.Connected -= OnDataNodeInputConnected;
                input.Disconnecting -= OnDataNodeInputDisconnecting;
            }

            RemoveNode(node);
        }
        
        private void OnDataNodeInputConnected(object sender, IConnection connection)
        {
            ObserveNodeInputs(connection.Output.Node, _nodesDepth[connection.Input.Node.Guid] + 1);
            SortNodes();
            ValidateNodes();
        }

        private void OnDataNodeInputDisconnecting(object sender, IConnection connection)
        {
            OverlookNodeInputs(connection.Output.Node);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddNode(INode node)
        {
            _nodes.Add(node);
            _hasChanges = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RemoveNode(INode node)
        {
            _nodes.Remove(node);
            _hasChanges = true;
        }
    }
}