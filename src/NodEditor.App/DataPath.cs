using System;
using System.Collections.Generic;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class DataPath : IDataPath
    {
        private readonly List<INode> _nodes;
        private readonly IInputSocket _flowInput;
        private readonly Dictionary<Guid, int> _nodesDepth;

        private bool _canExecute;

        public DataPath(IInputSocket flowInput)
        {
            _flowInput = flowInput;

            _nodes = new List<INode>();
            _nodesDepth = new Dictionary<Guid, int>();
        }

        public void Construct()
        {
            SubscribeToNodeInputs(_flowInput.Connection.Output.Node, 0);
            ValidateNodes();
        }

        public void Execute()
        {
            if (_canExecute == false)
            {
                return;
            }

            for (var i = 0; i < _nodes.Count; i++)
            {
                // TODO: Skip if no changes.
                _nodes[i].Execute();
            }
        }

        public void Reset()
        {
            UnsubscribeFromNodeInputs(_flowInput.Connection.Output.Node);
        }

        private void SubscribeToNodeInputs(INode node, int depth)
        {
            if (IsNodeAdded(node.Guid))
            {
                return;
            }

            _nodes.Insert(IndexFor(depth), node);
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
            ValidateNodes();
        }

        private void OnDataNodeInputDisconnecting(object sender, IConnection connection)
        {
            UnsubscribeFromNodeInputs(connection.Output.Node);
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

        /// <summary>
        /// Binary search for node index by depth in descending order.
        /// </summary>
        private int IndexFor(int newNodeDepth)
        {
            var leftIndex = 0;
            var rightIndex = _nodes.Count;

            while (rightIndex - leftIndex > 0)
            {
                var middleIndex = (leftIndex + rightIndex) / 2;
                var middleNodeDepth = _nodesDepth[_nodes[middleIndex].Guid];

                if (newNodeDepth == middleNodeDepth)
                {
                    return middleIndex;
                }

                if (newNodeDepth > middleNodeDepth)
                {
                    rightIndex = middleIndex;
                }
                else
                {
                    leftIndex = middleIndex + 1;
                }
            }

            return leftIndex;
        }
    }
}