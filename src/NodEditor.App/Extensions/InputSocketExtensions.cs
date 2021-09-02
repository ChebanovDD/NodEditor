using NodEditor.Core.Interfaces;

namespace NodEditor.App.Extensions
{
    public static class InputSocketExtensions
    {
        public static IDataPath GetDataPath(this IInputSocket input)
        {
            return ((IFlowNode)input.Node).DataPaths[input.ElementIndex];
        }
    }
}