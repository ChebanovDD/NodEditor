namespace NodEditor.App.Interfaces
{
    public interface IGraphConstructor
    {
        void RegisterDataNode(IDataNode dataNode);
        void UnregisterDataNode(IDataNode dataNode);

        void RegisterFlowNode(IFlowNode flowNode);
        void UnregisterFlowNode(IFlowNode flowNode);
        
        void RegisterStartNode(IFlowNode startNode);
        void UnregisterStartNode();
        
        void RegisterUpdateNode(IFlowNode updateNode);
        void UnregisterUpdateNode();

        void Construct();
    }
}