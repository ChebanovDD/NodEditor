using NodEditor.App.Nodes;
using NodEditor.Core.Interfaces;

namespace NodEditor.UnitTests.Nodes
{
    public class MockDataNode : DataNode
    {
        public MockDataNode() : base(nameof(MockDataNode))
        {
        }

        public void AddInputsTest(params IInputSocket[] inputs)
        {
            AddInputs(inputs);
        }

        public void AddOutputTest(IOutputSocket output)
        {
            AddOutput(output);
        }

        protected override void OnExecute()
        {
        }
    }
}