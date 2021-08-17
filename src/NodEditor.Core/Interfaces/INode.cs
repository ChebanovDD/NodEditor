using System.Collections.Generic;

namespace NodEditor.Core.Interfaces
{
    public interface INode
    {
        int FactoryIndex { get; set; }
        bool HasInputs { get; }
        bool HasOutput { get; }
        IReadOnlyList<IInputSocket> Inputs { get; }
        IOutputSocket Output { get; }

        INode AddInput(IInputSocket input);
        INode AddOutput(IOutputSocket output);

        T GetInputValue<T>(int index);
        T GetOutputValue<T>();
        void SetOutputValue<T>(T value);
    }
}