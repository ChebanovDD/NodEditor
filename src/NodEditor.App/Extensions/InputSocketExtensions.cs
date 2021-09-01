using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Extensions
{
    public static class InputSocketExtensions
    {
        public static DataPath GetDataPath(this IInputSocket input)
        {
            return ((IFlowNode)input.Node).DataPaths[input.ElementIndex];
        }
    }
}