using System;
using NodEditor.App.Nodes;
using NodEditor.App.Sockets;
using NodEditor.Core.Interfaces;

namespace NodEditor.Variables
{
    internal class GetVariableNode<T> : DataNode
    {
        private readonly Variable<T> _variable;
        private readonly OutputSocket<T> _output;

        public GetVariableNode(IVariable variable) : base(variable.Name, Guid.NewGuid())
        {
            _variable = (Variable<T>)variable;
            _output = new OutputSocket<T>(_variable.Value);
            
            AddOutput(_output);
        }

        protected override void OnExecute()
        {
            _output.Value = _variable.Value;
        }
    }
}