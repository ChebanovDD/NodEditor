using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class Node : INode
    {
        private IInputSocket[] _inputs;
        private IOutputSocket _output;
        
        public int FactoryIndex { get; set; }
        public bool HasInputs => _inputs.Length > 0;
        public bool HasOutput => _output != null;
        
        public IReadOnlyList<IInputSocket> Inputs => _inputs;
        public IOutputSocket Output => _output;

        public T GetInputValue<T>(int index)
        {
            return ((InputSocket<T>)_inputs[index]).Value;
        }

        public T GetOutputValue<T>()
        {
            return ((OutputSocket<T>)_output).Value;
        }
        
        public void Execute()
        {
            OnExecute();
        }
        
        protected void AddInputs(params IInputSocket[] inputs)
        {
            _inputs = inputs ?? throw new SocketCanNotBeNullReferenceException();
        }
        
        protected void AddOutput(IOutputSocket output)
        {
            _output = output ?? throw new SocketCanNotBeNullReferenceException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnExecute();
    }
}