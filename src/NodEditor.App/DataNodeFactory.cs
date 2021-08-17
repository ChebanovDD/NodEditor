using NodEditor.App.Interfaces;
using NodEditor.App.Nodes;

namespace NodEditor.App
{
    public abstract class DataNodeFactory : NodeFactory<DataNode>, IDataNodeFactory
    {
        protected DataNodeFactory(string name) : base(name)
        {
        }
    }
}