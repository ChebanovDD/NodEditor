using NodEditor.App.Nodes;
using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.Nodes
{
    public class LogNode : FlowNode
    {
        private readonly InputSocket<object> _input = new();
        private readonly InputFlowSocket _inputFlow = new();
        private readonly OutputFlowSocket _outputFlow = new();

        public LogNode() : base(nameof(LogNode))
        {
            AddInputs(_input);
            AddInputFlow(_inputFlow);
            AddOutputFlows(_outputFlow);
        }
        
        public object GetLastValue()
        {
            return _input.Value;
        }
        
        protected override void OnExecute()
        {
        }
    }
}