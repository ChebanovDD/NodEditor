using System.Collections.Generic;
using NodEditor.App.Interfaces;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class FlowMachine : IFlowMachine
    {
        private readonly List<IFlowGraph> _flowGraphs = new();

        public void Update()
        {
            for (var i = 0; i < _flowGraphs.Count; i++)
            {
                _flowGraphs[i].Update();
            }
        }
        
        public void RegisterFlowGraph(IFlowGraph flowGraph)
        {
            if (_flowGraphs.Contains(flowGraph))
            {
                throw new FlowGraphAlreadyRegisteredException(flowGraph);
            }
            
            _flowGraphs.Add(flowGraph);
        }
        
        public void UnregisterFlowGraph(IFlowGraph flowGraph)
        {
            _flowGraphs.Remove(flowGraph);
        }
    }
}