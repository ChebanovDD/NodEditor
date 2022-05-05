﻿using System;
using System.Collections.Generic;
using NodEditor.App;
using NodEditor.App.Interfaces;
using NodEditor.Core.Extensions;
using NodEditor.Core.Interfaces;
using NodEditor.Variables;

namespace NodEditor
{
    public class FlowGraph : IFlowGraph
    {
        private readonly IGraphConstructor _graphConstructor = new GraphConstructor();

        private IFlowNode _startNode;
        private IFlowNode _updateNode;
        private Dictionary<Guid, IVariable> _variables = new();

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
                _graphConstructor.RegisterFlowNode((IFlowNode)node);
            }

            return this;
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

        public void RemoveNode()
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

            if (entryNode.OutputFlows.Length != 1)
            {
                throw new NotImplementedException();
            }

            return true;
        }
    }
}