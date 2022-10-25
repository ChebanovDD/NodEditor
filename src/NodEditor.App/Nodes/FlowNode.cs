using System;
using System.Runtime.CompilerServices;
using NodEditor.App.Internal.Controllers;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class FlowNode : DataNode, IFlowNode
    {
        private ReadOnlyArray<IDataPath> _dataPaths;
        private readonly InputFlowController _inputFlowController;
        private readonly OutputFlowController _outputFlowController;

        public override bool IsFlowNode => true;
        public bool HasInputFlow => _inputFlowController.HasSocket;
        public bool HasOutputFlows => _outputFlowController.HasSockets;

        public IInputFlowSocket InputFlow => _inputFlowController.Socket;
        public ReadOnlyArray<IOutputFlowSocket> OutputFlows => _outputFlowController.Sockets;

        public ReadOnlyArray<IDataPath> DataPaths => _dataPaths ??= CreateDataPaths();

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
            OnExecute(ProcessData(DataPaths));
        }

        protected override void OnExecute()
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnExecute(bool allDataPathsExecuted);
        
        private bool ProcessData(ReadOnlyArray<IDataPath> dataPaths)
        {
            if (dataPaths.Length == 1)
            {
                return dataPaths[0].Execute();
            }
            
            for (var i = 0; i < dataPaths.Length; i++)
            {
                if (dataPaths[i].Execute())
                {
                    continue;
                }

                return false;
            }

            return true;
        }
        
        private ReadOnlyArray<IDataPath> CreateDataPaths()
        {
            if (HasInputs == false)
            {
                return ReadOnlyArray<IDataPath>.Empty();
            }

            var dataPaths = new IDataPath[Inputs.Length];
            for (var i = 0; i < Inputs.Length; i++)
            {
                dataPaths[i] = new DataPath(Inputs[i]);
            }
            
            return new ReadOnlyArray<IDataPath>(dataPaths);
        }
    }
}