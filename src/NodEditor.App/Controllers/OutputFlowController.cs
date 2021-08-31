using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal class OutputFlowController : SocketsController<IOutputFlowSocket>
    {
        public OutputFlowController(INode node) : base(node)
        {
        }
    }
}