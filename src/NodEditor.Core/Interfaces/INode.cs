namespace NodEditor.Core.Interfaces
{
    public interface INode
    {
        int FactoryIndex { get; set; }
        bool HasInputs { get; }
        bool HasOutput { get; }
        
        ReadOnlyArray Inputs { get; }
        IOutputSocket Output { get; }
        
        T GetInputValue<T>(int index);
        T GetOutputValue<T>();
        
        void Execute();
    }
}