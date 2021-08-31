using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal class InputFlowController : SocketController<IInputFlowSocket>
    {
        public InputFlowController(INode node) : base(node)
        {
        }
    }
}