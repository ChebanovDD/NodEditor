using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.DataNodes
{
    public class ValueNode<T> : TestableDataNode
    {
        private readonly OutputSocket<T> _output = new();

        public ValueNode(T outputValue) : base(nameof(T))
        {
            OutputValue = outputValue;
            AddOutput(_output);
        }
        
        public T OutputValue { get; set; }
        
        protected override void OnExecute()
        {
            base.OnExecute();
            _output.Value = OutputValue;
        }
    }
}