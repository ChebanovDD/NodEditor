using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.Nodes
{
    public class ValueNode<T> : TestableDataNode
    {
        private readonly T _outputValue;
        private readonly OutputSocket<T> _output = new();

        public ValueNode(T outputValue) : base(nameof(T))
        {
            _outputValue = outputValue;
            AddOutput(_output);
        }
        
        protected override void OnExecute()
        {
            base.OnExecute();
            _output.Value = _outputValue;
        }
    }
}