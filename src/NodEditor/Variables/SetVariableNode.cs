using System;
using NodEditor.App.Nodes;
using NodEditor.App.Sockets;
using NodEditor.Core.Interfaces;

namespace NodEditor.Variables
{
    internal class SetVariableNode<T> : FlowNode
    {
        private readonly Variable<T> _variable;
        private readonly InputSocket<T> _input = new();
        private readonly InputFlowSocket _inputFlow = new();
        private readonly OutputFlowSocket _outputFlow = new();

        public SetVariableNode(IVariable variable) : base(variable.Name, Guid.NewGuid())
        {
            _variable = (Variable<T>)variable;
            AddInputs(_input);
            AddInputFlow(_inputFlow);
            AddOutputFlows(_outputFlow);
        }

        protected override void OnExecute()
        {
            if (_input.HasValue)
            {
                _variable.Value = _input.Value;
            }
            
            _outputFlow.Open();
        }
    }
}