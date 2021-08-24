using NodEditor.Core.Interfaces;
using NodEditor.Nodes;

namespace NodEditor.UnitTests.Nodes
{
    public class TestDataNode : DataNode
    {
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