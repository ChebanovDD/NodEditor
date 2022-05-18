using NodEditor.App.Nodes;

namespace NodEditor.UnitTests.DataNodes
{
    public abstract class TestableDataNode : DataNode
    {
        protected TestableDataNode(string name) : base(name)
        {
        }

        public bool IsExecuted { get; private set; }

        protected override void OnExecute()
        {
            IsExecuted = true;
        }
    }
}