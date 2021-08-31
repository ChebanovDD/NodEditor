using NodEditor.Core.Interfaces;

namespace NodEditor.App.Interfaces
{
    public interface IFlowMachine
    {
        void Update();
        void RegisterFlowGraph(IFlowGraph flowGraph);
        void UnregisterFlowGraph(IFlowGraph flowGraph);
    }
}