using System;
using System.Runtime.CompilerServices;
using NodEditor.App.Controllers;
using NodEditor.App.Sockets;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class Node : INode
    {
        private readonly InputDataController _inputDataController;
        private readonly OutputDataController _outputDataController;
        
        public Guid Guid { get; }
        public string Name { get; }
        public int FactoryIndex { set; get; }
        public bool HasInputs => _inputDataController.HasSockets;
        public bool HasOutput => _outputDataController.HasSocket;
        public bool CanExecute => _inputDataController.AllInputsConnected;

        public ReadOnlyArray<IInputSocket> Inputs => _inputDataController.Sockets;
        public IOutputSocket Output => _outputDataController.Socket;

        protected Node(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
            
            _inputDataController = new InputDataController(this);
            _outputDataController = new OutputDataController(this);
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
            _inputDataController.Add(inputs);
        }
        
        protected void AddOutput(IOutputSocket output)
        {
            _outputDataController.Add(output);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnExecute();
    }
}