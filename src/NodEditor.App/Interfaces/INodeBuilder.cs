namespace NodEditor.App.Interfaces
{
    public interface INodeBuilder<out T>
    {
        T NewNode();
    }
}