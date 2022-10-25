using NodEditor.App.Nodes;
using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.FlowNodes
{
    public class IfNode : FlowNode
    {
        private readonly InputSocket<bool> _inputCondition = new();
        private readonly InputFlowSocket _inputFlow = new();
        private readonly OutputFlowSocket _outputFlowTrue = new();
        private readonly OutputFlowSocket _outputFlowFalse = new();
        private readonly OutputFlowSocket _outputFlowThen = new();
        
        public IfNode(string name) : base(name)
        {
            AddInputs(_inputCondition);
            AddInputFlow(_inputFlow);
            AddOutputFlows(_outputFlowTrue, _outputFlowFalse, _outputFlowThen);
        }

        protected override void OnExecute(bool allDataPathsExecuted)
        {
            if (_inputCondition.HasValue == false || allDataPathsExecuted == false)
            {
                _outputFlowThen.Open();
                return;
            }

            if (_inputCondition.Value)
            {
                _outputFlowTrue.Open();
            }
            else
            {
                _outputFlowFalse.Open();
            }
            
            _outputFlowThen.Open();
        }
    }
}