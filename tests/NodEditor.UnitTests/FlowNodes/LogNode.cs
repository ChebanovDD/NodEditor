using System.Collections.Generic;
using NodEditor.App.Nodes;
using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.FlowNodes
{
    public class LogNode<T> : FlowNode
    {
        private readonly List<T> _values = new();
        private readonly InputSocket<T> _input = new();
        private readonly InputFlowSocket _inputFlow = new();
        private readonly OutputFlowSocket _outputFlow = new();

        public LogNode(string name) : base(name)
        {
            AddInputs(_input);
            AddInputFlow(_inputFlow);
            AddOutputFlows(_outputFlow);
        }

        public IReadOnlyList<T> Values => _values;
        public bool IsExecuted { get; private set; }

        protected override void OnExecute()
        {
            IsExecuted = true;

            _outputFlow.Open();
            _values.Add(_input.Value);
        }
    }
}