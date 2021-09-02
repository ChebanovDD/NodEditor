namespace NodEditor.Core.Interfaces
{
    public interface IFlowNode : IDataNode
    {
        bool HasInputFlow { get; }
        bool HasOutputFlows { get; }

        IInputFlowSocket InputFlow { get; }
        ReadOnlyArray<IOutputFlowSocket> OutputFlows { get; }
        ReadOnlyArray<IDataPath> DataPaths { get; }
    }
}