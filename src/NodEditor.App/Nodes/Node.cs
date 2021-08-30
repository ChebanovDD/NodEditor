using System;
using System.Runtime.CompilerServices;
using NodEditor.App.Sockets;
using NodEditor.Core;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class Node : INode
    {
        public Guid Guid { get; }
        public string Name { get; }
        public bool HasInputs => Inputs.Count > 0;
        public bool HasOutput => Output != null;
        public int FactoryIndex { set; get; }

        public ReadOnlyArray<IInputSocket> Inputs { get; private set; }
        public IOutputSocket Output { get; private set; }

        protected Node(string name)
        {
            Name = name;
            Guid = Guid.NewGuid();
        }

        protected Node(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }

        public T GetInputValue<T>(int index)
        {
            return ((InputSocket<T>)Inputs[index]).Value;
        }

        public T GetOutputValue<T>()
        {
            return ((OutputSocket<T>)Output).Value;
        }
        
        public void Execute()
        {
            OnExecute();
        }
        
        protected void AddInputs(params IInputSocket[] inputs)
        {
            if (inputs == null)
            {
                throw new SocketCanNotBeNullReferenceException();
            }

            Inputs = new ReadOnlyArray<IInputSocket>(inputs);
        }
        
        protected void AddOutput(IOutputSocket output)
        {
            Output = output ?? throw new SocketCanNotBeNullReferenceException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnExecute();
    }
}