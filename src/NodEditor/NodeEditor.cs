using NodEditor.App;
using NodEditor.App.Interfaces;

namespace NodEditor
{
    public class NodeEditor : Editor
    {
        public NodeEditor(IFlowManager flowManager, IConnector connector) 
            : base(flowManager, connector)
        {
        }
    }
}