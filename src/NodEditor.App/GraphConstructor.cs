using System.Collections.Generic;
using NodEditor.App.Interfaces;

namespace NodEditor.App
{
    public class GraphConstructor : IGraphConstructor
    {
        private IFlowNode _startNode;
        private IFlowNode _updateNode;

        private List<IDataNode> _dataNodes;
        private List<IFlowNode> _flowNodes;

        private List<IFlowNode> _flowPath;

        public void RegisterDataNode(IDataNode dataNode)
        {
            _dataNodes.Add(dataNode);
        }

        public void UnregisterDataNode(IDataNode dataNode)
        {
            _dataNodes.Remove(dataNode);
        }

        public void RegisterFlowNode(IFlowNode flowNode)
        {
            _flowNodes.Add(flowNode);
        }

        public void UnregisterFlowNode(IFlowNode flowNode)
        {
            _flowNodes.Remove(flowNode);
        }

        public void RegisterStartNode(IFlowNode startNode)
        {
            if (_startNode != null)
            {
                throw new System.NotImplementedException();
            }

            _startNode = startNode;
        }

        public void UnregisterStartNode()
        {
            if (_startNode == null)
            {
                throw new System.NotImplementedException();
            }
            
            // _startNode.InputFlow.Connected
            _startNode = null;
        }

        public void RegisterUpdateNode(IFlowNode updateNode)
        {
            _updateNode = updateNode;
        }

        public void UnregisterUpdateNode()
        {
            if (_updateNode == null)
            {
                throw new System.NotImplementedException();
            }
            
            _updateNode = null;
        }

        public void Construct()
        {
            ConstructGraph(_startNode);
            ConstructGraph(_updateNode);
        }

        private void ConstructGraph(IFlowNode entryNode)
        {
            if (entryNode == null)
            {
                return;
            }
        }
    }
}