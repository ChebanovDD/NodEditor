using System;
using NodEditor.App.Nodes;
using NodEditor.App.Sockets;
using NodEditor.Core.Interfaces;

namespace NodEditor.Variables
{
    internal class SetVariableNode<T> : FlowNode
    {
        private readonly Variable<T> _variable;
        private readonly InputSocket<T> _input;
        private readonly OutputFlowSocket _outputFlow;

        public SetVariableNode(IVariable variable) : base(variable.Name, Guid.NewGuid())
        {
            _variable = (Variable<T>) variable;
            _input = new InputSocket<T>();
            _outputFlow = new OutputFlowSocket();

            AddInputs(_input);
            AddInputFlow(new InputFlowSocket());
            AddOutputFlows(_outputFlow);
        }

        protected override void OnExecute(bool allDataPathsExecuted)
        {
            if (_input.HasValue && allDataPathsExecuted)
            {
                _variable.Value = _input.Value;
            }
            
            _outputFlow.Open();
        }
    }
}