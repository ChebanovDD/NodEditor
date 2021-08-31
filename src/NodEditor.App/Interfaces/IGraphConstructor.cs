namespace NodEditor.App.Interfaces
{
    public interface IGraphConstructor
    {
        void RegisterStartNode(IFlowNode startNode);
        void UnregisterStartNode();
        
        void RegisterUpdateNode(IFlowNode updateNode);
        void UnregisterUpdateNode();
        
        void RegisterFlowNode(IFlowNode flowNode);
        void UnregisterFlowNode(IFlowNode flowNode);
        
        void RegisterDataNode(IDataNode dataNode);
        void UnregisterDataNode(IDataNode dataNode);

        void Construct();
    }
}