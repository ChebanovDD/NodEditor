using System.Collections.Generic;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class Node : INode
    {
        private IOutputSocket _output;
        private readonly List<IInputSocket> _inputs = new();

        public int FactoryIndex { get; set; }
        public bool HasInputs => _inputs.Count > 0;
        public bool HasOutput => _output != null;
        
        public IReadOnlyList<IInputSocket> Inputs => _inputs;
        public IOutputSocket Output => _output;

        public INode AddInput(IInputSocket input)
        {
            if (input == null)
            {
                throw new SocketNullReferenceException();
            }

            _inputs.Add(input);
            return this;
        }

        public INode AddOutput(IOutputSocket output)
        {
            _output = output ?? throw new SocketNullReferenceException();
            return this;
        }

        public T GetInputValue<T>(int index)
        {
            return ((InputSocket<T>)_inputs[index]).GetValue();
        }

        public T GetOutputValue<T>()
        {
            if (_output == null)
            {
                throw new SocketNullReferenceException();
            }

            return ((OutputSocket<T>)_output).GetValue();
        }

        public void SetOutputValue<T>(T value)
        {
            ((OutputSocket<T>)_output).SetValue(value);
        }
    }
}