using System;
using NodEditor.App;
using NodEditor.App.Interfaces;
using NodEditor.Core.Extensions;
using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class FlowGraph : IFlowGraph
    {
        private readonly IGraphConstructor _graphConstructor = new GraphConstructor();
        
        public Guid Guid { get; }
        public string Name { get; }
        public bool IsEnabled { get; private set; }

        public event EventHandler Enabled;
        public event EventHandler Disabled;

        public FlowGraph(string name) : this(name, Guid.NewGuid())
        {
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
            if (IsStartNode(node, out var startNode))
            {
                _graphConstructor.RegisterStartNode(startNode);
            }
            else if (IsUpdateNode(node, out var updateNode))
            {
                _graphConstructor.RegisterUpdateNode(updateNode);
            }
            else if (node.IsFlowNode)
            {
                _graphConstructor.RegisterFlowNode((IFlowNode)node);
            }
            else
            {
                _graphConstructor.RegisterDataNode((IDataNode)node);
            }

            return this;
        }

        public void RemoveNode()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        
        // TODO: Move to INodeValidator.
        private bool IsStartNode(INode node, out IFlowNode startNode)
        {
            if (node.IsStartNode())
            {
                return IsEntryNode(node, out startNode);
            }
            
            startNode = default;
            return false;
        }
        
        private bool IsUpdateNode(INode node, out IFlowNode updateNode)
        {
            if (node.IsUpdateNode())
            {
                return IsEntryNode(node, out updateNode);
            }
            
            updateNode = default;
            return false;
        }

        private bool IsEntryNode(INode node, out IFlowNode entryNode)
        {
            if (node.HasInputs || node.HasOutput || node.IsFlowNode == false)
            {
                throw new NotImplementedException();
            }

            entryNode = (IFlowNode)node;

            if (entryNode.HasInputFlow || entryNode.HasOutputFlows == false)
            {
                throw new NotImplementedException();
            }

            if (entryNode.OutputFlows.Count != 1)
            {
                throw new NotImplementedException();
            }

            return true;
        }
    }
}