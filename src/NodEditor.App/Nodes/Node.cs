using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodEditor.App.Extensions;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class Node : INode
    {
        private readonly List<IInputSocket> _input = new();

        public int FactoryIndex { get; set; }
        public bool HasInputs => _input.Count > 0;
        public bool HasOutput => Output != null;
        public IReadOnlyList<IInputSocket> Inputs => _input;
        public IOutputSocket Output { get; private set; }

        public INode AddInput(IInputSocket input)
        {
            if (input == null)
            {
                throw new SocketNullReferenceException();
            }

            _input.Add(input);
            return this;
        }

        public INode AddOutput(IOutputSocket output)
        {
            Output = output ?? throw new SocketNullReferenceException();
            return this;
        }

        public T GetInputValue<T>(int index)
        {
            return GetInput<T>(index).GetValue();
        }

        public T GetOutputValue<T>()
        {
            return GetOutput().GetValue<T>();
        }

        public void SetOutputValue<T>(T value)
        {
            GetOutput().SetValue<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private InputSocket<T> GetInput<T>(int index)
        {
            if (HasInput(index))
            {
                return (InputSocket<T>)Inputs[index];
            }

            throw new SocketNullReferenceException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasInput(int index)
        {
            return HasInputs && 0 <= index && index < _input.Count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IOutputSocket GetOutput()
        {
            if (HasOutput)
            {
                return Output;
            }

            throw new SocketNullReferenceException();
        }
    }
}