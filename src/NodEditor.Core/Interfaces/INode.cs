using System;

namespace NodEditor.Core.Interfaces
{
    public interface INode
    {
        Guid Guid { get; }
        string Name { get; }
        int FactoryIndex { get; set; }
        bool HasInputs { get; }
        bool HasOutput { get; }
        bool AllInputsReady { get; }
        
        ReadOnlyArray<IInputSocket> Inputs { get; }
        IOutputSocket Output { get; }
        
        T GetInputValue<T>(int index);
        T GetOutputValue<T>();
        
        void Execute();
    }
}