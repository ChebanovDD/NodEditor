namespace NodEditor.Core.Interfaces
{
    public interface INode
    {
        int FactoryIndex { get; set; }
        bool HasOutput { get; }
        IOutputSocket Output { get; }
        
        void AddOutput(IOutputSocket output);
        
        T GetOutputValue<T>();
        void SetOutputValue<T>(T value);
    }
}