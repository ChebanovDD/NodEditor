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

        void Start();
        void Update();

        IFlowGraph AddNode(INode node);
        IConnection Connect(IOutputSocket output, IInputSocket input);
        void Disconnect(IConnection connection);
        void RemoveNode(Guid nodeGuid);

        IFlowGraph RegisterVariable(IVariable variable);
        INode CreateGetVariableNode<T>(Guid variableGuid);
        IFlowNode CreateSetVariableNode<T>(Guid variableGuid);
    }
}