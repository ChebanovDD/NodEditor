using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public abstract class NodeFactory<T> : INodeFactory, INodeBuilder<T>, INodeExecutor<T> where T : INode
    {
        public int Index { get; set; }
        public string Name { get; }

        protected NodeFactory(string name)
        {
            Name = name;
        }

        public T NewNode()
        {
            var node = CreateNode();
            node.FactoryIndex = Index;

            return BuildNode(node);
        }

        protected abstract T CreateNode();
        protected abstract T BuildNode(T node);
        public abstract void ExecuteNode(T node);
    }
}