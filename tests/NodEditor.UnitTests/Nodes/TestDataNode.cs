using NodEditor.App.Nodes;
using NodEditor.Core.Interfaces;

namespace NodEditor.UnitTests.Nodes
{
    public class TestDataNode : DataNode
    {
        private IOutputSocket _output;
        
        public TestDataNode() : base(nameof(TestDataNode))
        {
        }

        public void AddInputsTest(params IInputSocket[] inputs)
        {
            AddInputs(inputs);
        }

        public void AddOutputTest(IOutputSocket output)
        {
            _output = output;
            AddOutput(output);
        }

        protected override void OnExecute()
        {
        }
    }
}