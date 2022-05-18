using System;
using System.Collections.Generic;
using NodEditor.App.Extensions;
using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class GraphConstructor : IGraphConstructor
    {
        private readonly List<IFlowNode> _flowNodes;

        public GraphConstructor()
        {
            _flowNodes = new List<IFlowNode>();
        }
        
        public void RegisterFlowNode(IFlowNode flowNode)
        {
            if (_flowNodes.Contains(flowNode))
            {
                throw new NotImplementedException();
            }

            _flowNodes.Add(flowNode);
            SubscribeOnFlowNodeEvents(flowNode);
        }

        public void UnregisterFlowNode(IFlowNode flowNode)
        {
            if (_flowNodes.Remove(flowNode))
            {
                UnsubscribeFromFlowNodeEvents(flowNode);
            }
        }

        public void Construct()
        {
        }
        
        private void SubscribeOnFlowNodeEvents(IFlowNode flowNode)
        {
            for (var i = 0; i < flowNode.Inputs.Length; i++)
            {
                flowNode.Inputs[i].Connected += OnFlowNodeInputConnected;
                flowNode.Inputs[i].Disconnecting += OnFlowNodeInputDisconnecting;
            }
        }

        private void OnFlowNodeInputConnected(object sender, IConnection connection)
        {
            connection.Input.GetDataPath().Construct();
        }

        private void OnFlowNodeInputDisconnecting(object sender, IConnection connection)
        {
            connection.Input.GetDataPath().Reset();
        }

        private void UnsubscribeFromFlowNodeEvents(IFlowNode flowNode)
        {
            for (var i = 0; i < flowNode.Inputs.Length; i++)
            {
                flowNode.Inputs[i].Connected -= OnFlowNodeInputConnected;
                flowNode.Inputs[i].Disconnected -= OnFlowNodeInputDisconnecting;
            }
        }
    }
}