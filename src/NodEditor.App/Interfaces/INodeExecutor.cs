namespace NodEditor.App.Interfaces
{
    public interface INodeExecutor<in T>
    {
        void ExecuteNode(T node);
    }
}