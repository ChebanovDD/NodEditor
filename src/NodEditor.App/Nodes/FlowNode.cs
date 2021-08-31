using System;
using NodEditor.App.Controllers;
using NodEditor.App.Interfaces;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class FlowNode : DataNode, IFlowNode
    {
        private readonly InputFlowController _inputFlowController;
        private readonly OutputFlowController _outputFlowController;

        public override bool IsFlowNode => true;
        public bool HasInputFlow => _inputFlowController.HasSocket;
        public bool HasOutputFlows => _outputFlowController.HasSockets;

        public IInputFlowSocket InputFlow => _inputFlowController.Socket;
        public ReadOnlyArray<IOutputFlowSocket> OutputFlows => _outputFlowController.Sockets;

        protected FlowNode(string name) : this(name, Guid.NewGuid())
        {
        }

        protected FlowNode(string name, Guid guid) : base(name, guid)
        {
            _inputFlowController = new InputFlowController(this);
            _outputFlowController = new OutputFlowController(this);
        }
        
        protected void AddInputFlow(IInputFlowSocket input)
        {
            _inputFlowController.Add(input);
        }
        
        protected void AddOutputFlows(params IOutputFlowSocket[] outputFlows)
        {
            _outputFlowController.Add(outputFlows);
        }
    }
}