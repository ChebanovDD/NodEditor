using NodEditor.App.Interfaces;

namespace NodEditor.App
{
    public abstract class Editor : INodeEditor
    {
        private readonly IFlowManager _flowManager;
        
        protected Editor(IFlowManager flowManager)
        {
            _flowManager = flowManager;
        }
    }
}