using NodEditor.Core.Interfaces;

namespace NodEditor.App.Internal.Controllers
{
    internal class InputFlowController : SocketController<IInputFlowSocket>
    {
        public InputFlowController(INode node) : base(node)
        {
        }
    }
}