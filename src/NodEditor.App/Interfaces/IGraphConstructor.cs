namespace NodEditor.App.Interfaces
{
    public interface IGraphConstructor
    {
        void RegisterFlowNode(IFlowNode flowNode);
        void UnregisterFlowNode(IFlowNode flowNode);

        void Construct();
    }
}