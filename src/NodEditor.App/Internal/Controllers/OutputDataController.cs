using NodEditor.Core.Interfaces;

namespace NodEditor.App.Internal.Controllers
{
    internal class OutputDataController : SocketController<IOutputSocket>
    {
        public OutputDataController(INode node) : base(node)
        {
        }
    }
}