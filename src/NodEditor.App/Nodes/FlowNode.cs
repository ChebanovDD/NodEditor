using System;
using NodEditor.App.Controllers;
using NodEditor.App.Interfaces;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class FlowNode : DataNode, IFlowNode
    {
        private InputFlowController _inputFlowController;
        private OutputFlowController _outputFlowController;
        
        public bool HasInputFlow => _inputFlowController.HasSocket;
        public bool HasOutputFlows => _outputFlowController.HasSockets;

        public IInputFlowSocket InputFlow => _inputFlowController.Socket;
        public ReadOnlyArray<IOutputFlowSocket> OutputFlows => _outputFlowController.Sockets;

        protected FlowNode(string name) : base(name)
        {
            InitializeFlowControllers();
        }

        protected FlowNode(string name, Guid guid) : base(name, guid)
        {
            InitializeFlowControllers();
        }
        
        private void InitializeFlowControllers()
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