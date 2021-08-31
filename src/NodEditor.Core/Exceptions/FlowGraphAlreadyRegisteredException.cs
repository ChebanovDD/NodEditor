using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.Core.Exceptions
{
    public class FlowGraphAlreadyRegisteredException : Exception
    {
        public FlowGraphAlreadyRegisteredException(IFlowGraph flowGraph)
            : base($"FlowGraph '{flowGraph.Name}' already registered.")
        {
        }
    }
}