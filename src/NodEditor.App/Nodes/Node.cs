using System.Runtime.CompilerServices;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class Node : INode
    {
        public int FactoryIndex { get; set; }
        public bool HasOutput => Output != null;
        public IOutputSocket Output { get; private set; }

        public void AddOutput(IOutputSocket output)
        {
            Output = output ?? throw new SocketNullReferenceException();
        }

        public T GetOutputValue<T>()
        {
            return GetOutput<T>().GetValue();
        }

        public void SetOutputValue<T>(T value)
        {
            GetOutput<T>().SetValue(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OutputSocket<T> GetOutput<T>()
        {
            if (HasOutput)
            {
                return (OutputSocket<T>)Output;
            }

            throw new SocketNullReferenceException();
        }
    }
}