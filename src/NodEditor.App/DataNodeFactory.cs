using NodEditor.App.Interfaces;
using NodEditor.App.Nodes;

namespace NodEditor.App
{
    public abstract class DataNodeFactory : NodeFactory<IDataNode>, IDataNodeFactory
    {
        protected DataNodeFactory(string name) : base(name)
        {
        }

        protected override IDataNode CreateNode()
        {
            return new DataNode();
        }
    }
}