using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal class OutputDataController : SocketController<IOutputSocket>
    {
        public OutputDataController(INode node) : base(node)
        {
        }
    }
}