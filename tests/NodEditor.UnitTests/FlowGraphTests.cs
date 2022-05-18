using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.UnitTests.DataNodes;
using NodEditor.UnitTests.FlowNodes;
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
            var valueNode1 = new ValueNode<float>(2.5f);
            var valueNode2 = new ValueNode<float>(5.5f);
            var sumNode1 = new SumNode("sumNode1");
            var sumNode2 = new SumNode("sumNode2");
            var logNode1 = new LogNode<float>("logNode1");
            var logNode2 = new LogNode<float>("logNode2");
            var ifNode = new IfNode(nameof(IfNode));
            var boolNode = new ValueNode<bool>(true);
            var trueLog = new LogNode<bool>("logTrue");
            var falseLog = new LogNode<bool>("logFalse");

            var flowGraph = new FlowGraph("FlowTest")
                .AddNode(startNode)
                .AddNode(valueNode1)
                .AddNode(valueNode2)
                .AddNode(sumNode1)
                .AddNode(sumNode2)
                .AddNode(logNode1)
                .AddNode(logNode2)
                .AddNode(ifNode)
                .AddNode(boolNode)
                .AddNode(trueLog)
                .AddNode(falseLog);

            // Act
            _nodeEditor.Connect(valueNode1.Output, sumNode1.Inputs[0]);
            _nodeEditor.Connect(valueNode2.Output, sumNode1.Inputs[1]);
            _nodeEditor.Connect(sumNode1.Output, logNode1.Inputs[0]);
            _nodeEditor.Connect(sumNode1.Output, sumNode2.Inputs[0]);
            _nodeEditor.Connect(sumNode1.Output, sumNode2.Inputs[1]);
            _nodeEditor.Connect(sumNode2.Output, logNode2.Inputs[0]);
            _nodeEditor.Connect(boolNode.Output, ifNode.Inputs[0]);

            _nodeEditor.Connect(startNode.OutputFlows[0], logNode1.InputFlow);
            _nodeEditor.Connect(logNode1.OutputFlows[0], ifNode.InputFlow);
            _nodeEditor.Connect(ifNode.OutputFlows[0], trueLog.InputFlow);
            _nodeEditor.Connect(ifNode.OutputFlows[1], falseLog.InputFlow);
            _nodeEditor.Connect(ifNode.OutputFlows[2], logNode2.InputFlow);

            flowGraph.Start();

            // Assert
            sumNode1.IsExecuted.Should().BeTrue();
            sumNode2.IsExecuted.Should().BeTrue();

            logNode1.IsExecuted.Should().BeTrue();
            logNode2.IsExecuted.Should().BeTrue();
            trueLog.IsExecuted.Should().BeTrue();
            falseLog.IsExecuted.Should().BeFalse();

            logNode1.GetLastValue().Should().Be(8.0f);
            logNode2.GetLastValue().Should().Be(16.0f);
        }

        [Fact]
        public void Start_ShouldNotExecuteFlow_WhenFlowIsLooped()
        {
            // Arrange
            var startNode = new StartNode();
            var valueNode = new ValueNode<float>(2.5f);
            var sumNode1 = new SumNode("sumNode1");
            var sumNode2 = new SumNode("sumNode2");
            var logNode = new LogNode<float>("logNode");

            var flowGraph = new FlowGraph("FlowTest")
                .AddNode(startNode)
                .AddNode(valueNode)
                .AddNode(sumNode1)
                .AddNode(sumNode2)
                .AddNode(logNode);

            // Act
            _nodeEditor.Connect(valueNode.Output, sumNode1.Inputs[0]);
            _nodeEditor.Connect(valueNode.Output, sumNode2.Inputs[1]);
            _nodeEditor.Connect(sumNode1.Output, sumNode2.Inputs[0]);
            _nodeEditor.Connect(sumNode2.Output, sumNode1.Inputs[1]);
            _nodeEditor.Connect(sumNode2.Output, logNode.Inputs[0]);

            _nodeEditor.Connect(startNode.OutputFlows[0], logNode.InputFlow);

            flowGraph.Start();

            // Assert
            logNode.IsExecuted.Should().BeTrue();
            sumNode1.IsExecuted.Should().BeFalse();
            sumNode2.IsExecuted.Should().BeFalse();
        }
    }
}