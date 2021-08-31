using NodEditor.App.Nodes;
using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.Nodes
{
    public class ValueNode : DataNode
    {
        private readonly float _outputValue;
        private readonly OutputSocket<float> _output = new();

        public ValueNode(float outputValue) : base(nameof(ValueNode))
        {
            _outputValue = outputValue;
            AddOutput(_output);
        }
        
        protected override void OnExecute()
        {
            _output.Value = _outputValue;
        }
    }
}