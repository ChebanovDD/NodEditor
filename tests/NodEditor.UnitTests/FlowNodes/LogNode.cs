using NodEditor.App.Nodes;
using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.FlowNodes
{
    public class LogNode<T> : FlowNode
    {
        private readonly InputSocket<T> _input = new();
        private readonly InputFlowSocket _inputFlow = new();
        private readonly OutputFlowSocket _outputFlow = new();

        public bool IsExecuted { get; private set; }
        
        public LogNode(string name) : base(name)
        {
            AddInputs(_input);
            AddInputFlow(_inputFlow);
            AddOutputFlows(_outputFlow);
        }
        
        public T GetLastValue()
        {
            return _input.Value;
        }
        
        protected override void OnExecute()
        {
            IsExecuted = true;
            _outputFlow.Open();
        }
    }
}