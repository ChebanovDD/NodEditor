using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class DataPath
    {
        private int _readyNodesCount;
        private readonly List<INode> _nodes = new();

        public Guid Guid { get; }
        
        public DataPath(Guid guid)
        {
            Guid = guid;
        }

        public void AddNode(INode node)
        {
            _nodes.Add(node);
            ValidateNode(node);
            SubscribeOnEvents(node);
        }

        public void RemoveNode(INode node)
        {
            _nodes.Remove(node);
            ValidateNode(node);
            UnsubscribeFromEvents(node);
        }

        public bool Execute()
        {
            if (CanExecute() == false)
            {
                return false;
            }
            
            ExecutePath();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanExecute()
        {
            return _readyNodesCount == _nodes.Count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ExecutePath()
        {
            // TODO: Skip if no changes.
            
            for (var i = _nodes.Count - 1; i >= 0; i--)
            {
                _nodes[i].Execute();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateNode(INode node)
        {
            if (node.AllInputsReady)
            {
                _readyNodesCount++;
            }
            else
            {
                _readyNodesCount--;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SubscribeOnEvents(INode node)
        {
            node.ReadyToExecute += OnNodeReadyToExecute;
            node.UnreadyToExecute += OnNodeUnreadyToExecute;
        }

        private void OnNodeReadyToExecute(object sender, EventArgs e)
        {
            _readyNodesCount++;
        }

        private void OnNodeUnreadyToExecute(object sender, EventArgs e)
        {
            _readyNodesCount--;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UnsubscribeFromEvents(INode node)
        {
            node.ReadyToExecute -= OnNodeReadyToExecute;
            node.UnreadyToExecute -= OnNodeUnreadyToExecute;
        }
    }
}