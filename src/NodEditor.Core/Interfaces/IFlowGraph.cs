using System;

namespace NodEditor.Core.Interfaces
{
    public interface IFlowGraph
    {
        Guid Guid { get; }
        string Name { get; }
        bool IsEnabled { get; }
        
        event EventHandler Enabled;
        event EventHandler Disabled;
        
        void Enable();
        void Disable();
        
        IFlowGraph AddNode(INode node);
        void RemoveNode();
        
        void Start();
        void Update();
    }
}