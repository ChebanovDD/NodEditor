using NodEditor.App.Nodes;
using NodEditor.App.Sockets;
using NodEditor.Core.Attributes;

namespace NodEditor.UnitTests.FlowNodes
{
    [StartNode]
    public class StartNode : FlowNode
    {
        private readonly OutputFlowSocket _outputFlow = new();

        public StartNode() : base(nameof(StartNode))
        {
            AddOutputFlows(_outputFlow);
        }
        
        protected override void OnExecute()
        {
            _outputFlow.Open();
        }
    }
}