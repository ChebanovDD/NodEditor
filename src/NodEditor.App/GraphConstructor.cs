using System;
using System.Collections.Generic;
using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class GraphConstructor : IGraphConstructor
    {
        private IFlowNode _startNode;
        private IFlowNode _updateNode;

        private List<IDataNode> _dataNodes;
        private List<IFlowNode> _flowNodes;

        private List<IFlowNode> _flowPath;

        private readonly Dictionary<Guid, DataPath> _dataPaths = new();

        public void RegisterStartNode(IFlowNode startNode)
        {
            if (_startNode != null)
            {
                throw new NotImplementedException();
            }

            _startNode = startNode ?? throw new NotImplementedException();
            _startNode.OutputFlows[0].Connected += OnStartNodeConnected;
            _startNode.OutputFlows[0].Disconnected += OnStartNodeDisconnected;
            _startNode.OutputFlows[0].SocketOpened += OnStartNodeExecuted;
        }

        private void OnStartNodeConnected(object sender, IConnection connection)
        {
            throw new NotImplementedException();
        }
        
        private void OnStartNodeExecuted(object sender, int socketIndex)
        {
            throw new NotImplementedException();
        }
        
        private void OnStartNodeDisconnected(object sender, IConnection connection)
        {
            throw new NotImplementedException();
        }

        public void UnregisterStartNode()
        {
            if (_startNode == null)
            {
                throw new NotImplementedException();
            }
            
            _startNode.OutputFlows[0].Connected -= OnStartNodeConnected;
            _startNode.OutputFlows[0].Disconnected -= OnStartNodeDisconnected;
            _startNode = null;
        }

        public void RegisterUpdateNode(IFlowNode updateNode)
        {
            if (_updateNode != null)
            {
                throw new System.NotImplementedException();
            }

            _updateNode = updateNode ?? throw new System.NotImplementedException();
        }

        public void UnregisterUpdateNode()
        {
            if (_updateNode == null)
            {
                throw new System.NotImplementedException();
            }
            
            _updateNode = null;
        }

        public void RegisterFlowNode(IFlowNode flowNode)
        {
            _flowNodes.Add(flowNode);
        }

        public void UnregisterFlowNode(IFlowNode flowNode)
        {
            _flowNodes.Remove(flowNode);
        }

        public void RegisterDataNode(IDataNode dataNode)
        {
            _dataNodes.Add(dataNode);
        }

        public void UnregisterDataNode(IDataNode dataNode)
        {
            _dataNodes.Remove(dataNode);
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