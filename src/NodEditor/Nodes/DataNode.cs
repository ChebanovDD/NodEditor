using System;
using NodEditor.App.Nodes;
using NodEditor.Interfaces;

namespace NodEditor.Nodes
{
    public abstract class DataNode : Node, IDataNode
    {
        protected DataNode() : base(nameof(DataNode))
        {
        }

        protected DataNode(string name) : base(name)
        {
        }

        protected DataNode(string name, Guid guid) : base(name, guid)
        {
        }
    }
}