using System;
using NodEditor.App.Controllers;
using NodEditor.App.Interfaces;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class FlowNode : DataNode, IFlowNode
    {
        private ReadOnlyArray<DataPath> _dataPaths;
        private readonly InputFlowController _inputFlowController;
        private readonly OutputFlowController _outputFlowController;

        public override bool IsFlowNode => true;
        public bool HasInputFlow => _inputFlowController.HasSocket;
        public bool HasOutputFlows => _outputFlowController.HasSockets;

        public IInputFlowSocket InputFlow => _inputFlowController.Socket;
        public ReadOnlyArray<IOutputFlowSocket> OutputFlows => _outputFlowController.Sockets;

        public ReadOnlyArray<DataPath> DataPaths => _dataPaths ??= CreateDataPaths();

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

        public override void Execute()
        {
            ProcessData();
            base.Execute();
        }

        private void ProcessData()
        {
            if (_dataPaths == null)
            {
                return;
            }

            if (_dataPaths.Count == 1)
            {
                _dataPaths[0].Execute();
                return;
            }

            for (var i = 0; i < _dataPaths.Count; i++)
            {
                _dataPaths[i].Execute();
            }
        }
        
        private ReadOnlyArray<DataPath> CreateDataPaths()
        {
            if (HasInputs == false)
            {
                throw new NotImplementedException();
            }

            var dataPaths = new DataPath[Inputs.Count];
            for (var i = 0; i < Inputs.Count; i++)
            {
                dataPaths[i] = new DataPath(Inputs[i]);
            }
            
            return new ReadOnlyArray<DataPath>(dataPaths);
        }
    }
}