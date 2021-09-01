using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Interfaces
{
    public interface IFlowNode : IDataNode
    {
        bool HasInputFlow { get; }
        bool HasOutputFlows { get; }

        IInputFlowSocket InputFlow { get; }
        ReadOnlyArray<IOutputFlowSocket> OutputFlows { get; }
        ReadOnlyArray<DataPath> DataPaths { get; }
    }
}