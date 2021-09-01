using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.UnitTests.Nodes;
using Xunit;

namespace NodEditor.UnitTests
{
    public class FlowGraphTests
    {
        private readonly INodeEditor _nodeEditor;

        public FlowGraphTests()
        {
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }
        
        [Fact]
        public void Start_ShouldExecuteFlow_WhenFlowIsValid()
        {
            // Arrange
            var startNode = new StartNode();
            var valueNode1 = new ValueNode(2.5f);
            var valueNode2 = new ValueNode(5.5f);
            var sumNode1 = new SumNode("sumNode1");
            var sumNode2 = new SumNode("sumNode2");
            var logNode1 = new LogNode("logNode1");
            var logNode2 = new LogNode("logNode2");
            
            var flowGraph = new FlowGraph("SumFlowTest")
                .AddNode(startNode)
                .AddNode(valueNode1)
                .AddNode(valueNode2)
                .AddNode(sumNode1)
                .AddNode(logNode1)
                .AddNode(logNode2);
            
            // Act
            _nodeEditor.Connect(valueNode1.Output, sumNode1.Inputs[0]);
            _nodeEditor.Connect(valueNode2.Output, sumNode1.Inputs[1]);
            _nodeEditor.Connect(sumNode1.Output, logNode1.Inputs[0]);
            _nodeEditor.Connect(sumNode2.Output, logNode2.Inputs[0]);
            _nodeEditor.Connect(sumNode1.Output, sumNode2.Inputs[0]);
            _nodeEditor.Connect(sumNode1.Output, sumNode2.Inputs[1]);
            _nodeEditor.Connect(startNode.OutputFlows[0], logNode1.InputFlow);
            _nodeEditor.Connect(logNode1.OutputFlows[0], logNode2.InputFlow);
            
            flowGraph.Start();
            
            // Assert
            logNode1.GetLastValue().Should().Be(8.0f);
            logNode2.GetLastValue().Should().Be(16.0f);
        }
    }
}