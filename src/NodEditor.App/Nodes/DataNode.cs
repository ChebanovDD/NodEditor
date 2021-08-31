using System;
using NodEditor.App.Interfaces;

namespace NodEditor.App.Nodes
{
    public abstract class DataNode : Node, IDataNode
    {
        protected DataNode(string name) : base(name, Guid.NewGuid())
        {
        }
        
        protected DataNode(string name, Guid guid) : base(name, guid)
        {
        }
    }
}