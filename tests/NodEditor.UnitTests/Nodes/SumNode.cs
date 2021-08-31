using NodEditor.App.Nodes;
using NodEditor.App.Sockets;

namespace NodEditor.UnitTests.Nodes
{
    public class SumNode : DataNode
    {
        private readonly InputSocket<float> _input1 = new();
        private readonly InputSocket<float> _input2 = new();
        private readonly OutputSocket<float> _output = new();
        
        public SumNode() : base(nameof(SumNode))
        {
            AddInputs(_input1, _input2);
            AddOutput(_output);
        }
        
        protected override void OnExecute()
        {
            _output.Value = _input1.Value + _input2.Value;
        }
    }
}