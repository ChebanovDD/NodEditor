using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodEditor.App.Interfaces;
using NodEditor.App.Nodes;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class FlowManager : IFlowManager
    {
        private readonly INodeFactory[] _nodeFactories;
        private readonly Dictionary<Type, int> _factoryTypes = new();
        
        public FlowManager(INodeFactory[] nodeFactories)
        {
            _nodeFactories = nodeFactories;
            ConfigureNodeFactories();
        }
        
        public IDataNode NewDataNode<TFactory>() where TFactory : IDataNodeFactory
        {
            return GetFactory<TFactory, DataNode>().NewNode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private NodeFactory<T> GetFactory<TFactory, T>() where T : INode, new()
        {
            var factoryIndex = _factoryTypes[typeof(TFactory)];
            return (NodeFactory<T>)_nodeFactories[factoryIndex];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConfigureNodeFactories()
        {
            var count = _nodeFactories.Length;
            for (var i = 0; i < count; i++)
            {
                AddNodeFactory(_nodeFactories[i], i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddNodeFactory(INodeFactory nodeFactory, int index)
        {
            if (_factoryTypes.TryAdd(nodeFactory.GetType(), index) == false)
            {
                throw new ArgumentException($"Factory with name '{nodeFactory.Name}' exists.", nameof(nodeFactory));
            }
            
            nodeFactory.Index = index;
        }
    }
}