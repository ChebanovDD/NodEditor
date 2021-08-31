using System;
using NodEditor.Core.Extensions;
using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class FlowGraph : IFlowGraph
    {
        private INode _startNode;
        
        public Guid Guid { get; }
        public string Name { get; }
        public bool IsEnabled { get; private set; }

        public event EventHandler Enabled;
        public event EventHandler Disabled;

        public FlowGraph(string name)
        {
            Name = name;
            Guid = Guid.NewGuid();
        }
        
        public FlowGraph(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }

        public void Enable()
        {
            if (IsEnabled)
            {
                return;
            }

            IsEnabled = true;
            Enabled?.Invoke(this, EventArgs.Empty);
        }

        public void Disable()
        {
            if (IsEnabled == false)
            {
                return;
            }

            IsEnabled = false;
            Disabled?.Invoke(this, EventArgs.Empty);
        }

        public IFlowGraph AddNode(INode node)
        {
            if (node.IsStartNode())
            {
                _startNode = node;
            }

            return this;
        }

        public void RemoveNode()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _startNode?.Execute();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}