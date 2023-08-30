using System;
using System.Collections.Generic;
using NodEditor.App;
using NodEditor.App.Interfaces;
using NodEditor.Core.Attributes;
using NodEditor.Core.Extensions;
using NodEditor.Core.Interfaces;
using NodEditor.Variables;

namespace NodEditor
{
    public class FlowGraph : IFlowGraph
    {
        private readonly IConnector _connector;
        private readonly IGraphConstructor _graphConstructor;

        private readonly Dictionary<Guid, INode> _nodes;
        private readonly Dictionary<Guid, IVariable> _variables;

        private IFlowNode _startNode;
        private IFlowNode _updateNode;

        public Guid Guid { get; }
        public string Name { get; }
        public bool IsEnabled { get; private set; }

        public event EventHandler Enabled;
        public event EventHandler Disabled;

        public FlowGraph(string name) : this(name, Guid.NewGuid(), new Connector())
        {
        }

        public FlowGraph(string name, IConnector connector) : this(name, Guid.NewGuid(), connector)
        {
        }

        public FlowGraph(string name, Guid guid, IConnector connector)
        {
            _connector = connector;
            _graphConstructor = new GraphConstructor();

            _nodes = new Dictionary<Guid, INode>();
            _variables = new Dictionary<Guid, IVariable>();

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

        public void Start()
        {
            _startNode?.Execute();
        }

        public void Update()
        {
            _updateNode?.Execute();
        }

        public IFlowGraph AddNode(INode node)
        {
            if (IsStartNode(node, out var startNode))
            {
                if (_startNode != null)
                {
                    throw new NotImplementedException();
                }

                _startNode = startNode ?? throw new NotImplementedException();
            }
            else if (IsUpdateNode(node, out var updateNode))
            {
                if (_updateNode != null)
                {
                    throw new NotImplementedException();
                }

                _updateNode = updateNode ?? throw new NotImplementedException();
            }
            else if (node.IsFlowNode)
            {
                _graphConstructor.RegisterFlowNode((IFlowNode) node);
            }

            _nodes.Add(node.Guid, node);

            return this;
        }

        public IConnection Connect(IOutputSocket output, IInputSocket input)
        {
            return _connector.Connect(output, input);
        }

        public void Disconnect(IConnection connection)
        {
            _connector.Disconnect(connection);
        }

        public IFlowGraph RegisterVariable(IVariable variable)
        {
            _variables.Add(variable.Guid, variable);
            return this;
        }

        public INode CreateGetVariableNode<T>(Guid variableGuid)
        {
            return new GetVariableNode<T>(_variables[variableGuid]);
        }

        public IFlowNode CreateSetVariableNode<T>(Guid variableGuid)
        {
            return new SetVariableNode<T>(_variables[variableGuid]);
        }

        public void RemoveNode(Guid nodeGuid)
        {
            _nodes.Remove(nodeGuid);
        }

        // TODO: Move to INodeValidator.
        private static bool IsStartNode(INode node, out IFlowNode startNode)
        {
            return IsEntryNode<StartNodeAttribute>(node, out startNode);
        }

        private static bool IsUpdateNode(INode node, out IFlowNode updateNode)
        {
            return IsEntryNode<UpdateNodeAttribute>(node, out updateNode);
        }

        private static bool IsEntryNode<T>(INode node, out IFlowNode entryNode) where T : EntryNodeAttribute
        {
            if (node.HasCustomAttribute<T>() == false)
            {
                entryNode = default;
                return false;
            }

            if (node.HasInputs || node.HasOutput || node.IsFlowNode == false)
            {
                throw new NotImplementedException();
            }

            entryNode = (IFlowNode) node;

            if (entryNode.HasInputFlow || entryNode.HasOutputFlows == false)
            {
                throw new NotImplementedException();
            }

            if (entryNode.OutputFlows.Length != 1)
            {
                throw new NotImplementedException();
            }

            return true;
        }
    }
}