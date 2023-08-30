using FluentAssertions;
using NodEditor.UnitTests.DataNodes;
using NodEditor.UnitTests.FlowNodes;
using Xunit;

namespace NodEditor.UnitTests
{
    public class FlowGraphTests
    {
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
            flowGraph.Connect(valueNode1.Output, sumNode1.Inputs[0]);
            flowGraph.Connect(valueNode2.Output, sumNode1.Inputs[1]);
            flowGraph.Connect(sumNode1.Output, logNode1.Inputs[0]);
            flowGraph.Connect(sumNode1.Output, sumNode2.Inputs[0]);
            flowGraph.Connect(sumNode1.Output, sumNode2.Inputs[1]);
            flowGraph.Connect(sumNode2.Output, logNode2.Inputs[0]);
            flowGraph.Connect(boolNode.Output, ifNode.Inputs[0]);

            flowGraph.Connect(startNode.OutputFlows[0], logNode1.InputFlow);
            flowGraph.Connect(logNode1.OutputFlows[0], ifNode.InputFlow);
            flowGraph.Connect(ifNode.OutputFlows[0], trueLog.InputFlow);
            flowGraph.Connect(ifNode.OutputFlows[1], falseLog.InputFlow);
            flowGraph.Connect(ifNode.OutputFlows[2], logNode2.InputFlow);

            flowGraph.Start();

            // Assert
            sumNode1.IsExecuted.Should().BeTrue();
            sumNode2.IsExecuted.Should().BeTrue();

            sumNode1.ExecutionCount.Should().Be(2); // TODO: Should be 1 after execution optimization.
            sumNode2.ExecutionCount.Should().Be(1);

            trueLog.IsExecuted.Should().BeTrue();
            falseLog.IsExecuted.Should().BeFalse();

            logNode1.IsExecuted.Should().BeTrue();
            logNode1.Values.Count.Should().Be(1);
            logNode1.Values[0].Should().Be(8.0f);

            logNode2.IsExecuted.Should().BeTrue();
            logNode2.Values.Count.Should().Be(1);
            logNode2.Values[0].Should().Be(16.0f);
        }

        [Fact]
        public void Start_ShouldExecuteFlow_WhenDataPathIsChanged()
        {
            // Arrange
            var startNode = new StartNode();
            var valueNode = new ValueNode<string>("Hello World!");
            var logNode = new LogNode<string>("LogNode");

            var flowGraph = new FlowGraph("FlowTest")
                .AddNode(startNode)
                .AddNode(valueNode)
                .AddNode(logNode);

            flowGraph.Connect(startNode.OutputFlows[0], logNode.InputFlow);
            flowGraph.Connect(valueNode.Output, logNode.Inputs[0]);

            flowGraph.Start();

            // Assert

            logNode.Values.Count.Should().Be(1);
            logNode.Values[0].Should().Be("Hello World!");


            // // Arrange
            // var startNode = new StartNode();
            // var valueNode1 = new ValueNode<float>(2.5f);
            // var valueNode2 = new ValueNode<float>(5.5f);
            // var sumNode1 = new SumNode("sumNode1");
            // var sumNode2 = new SumNode("sumNode2");
            // var logNode = new LogNode<float>("logNode");
            //
            // var flowGraph = new FlowGraph("FlowTest")
            //     .AddNode(startNode)
            //     .AddNode(valueNode1)
            //     .AddNode(valueNode2)
            //     .AddNode(sumNode1)
            //     .AddNode(sumNode2)
            //     .AddNode(logNode);
            //
            // flowGraph.Connect(valueNode1.Output, sumNode1.Inputs[0]);
            // flowGraph.Connect(valueNode1.Output, sumNode1.Inputs[1]);
            //
            // flowGraph.Connect(valueNode2.Output, sumNode2.Inputs[0]);
            // flowGraph.Connect(valueNode2.Output, sumNode2.Inputs[1]);
            //
            // flowGraph.Connect(startNode.OutputFlows[0], logNode.InputFlow);
            //
            // // Act
            // flowGraph.Connect(sumNode1.Output, logNode.Inputs[0]);
            // flowGraph.Start();
            //
            // flowGraph.Connect(sumNode2.Output, logNode.Inputs[0]);
            // flowGraph.Start();
            //
            // // Assert
            // sumNode1.IsExecuted.Should().BeTrue();
            // sumNode2.IsExecuted.Should().BeTrue();
            //
            // sumNode1.ExecutionCount.Should().Be(1);
            // sumNode2.ExecutionCount.Should().Be(1);
            //
            // logNode.Values.Count.Should().Be(2);
            // logNode.Values[0].Should().Be(5.0f);
            // logNode.Values[1].Should().Be(11.0f);
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
            flowGraph.Connect(valueNode.Output, sumNode1.Inputs[0]);
            flowGraph.Connect(valueNode.Output, sumNode2.Inputs[1]);
            flowGraph.Connect(sumNode1.Output, sumNode2.Inputs[0]);
            flowGraph.Connect(sumNode2.Output, sumNode1.Inputs[1]);
            flowGraph.Connect(sumNode2.Output, logNode.Inputs[0]);

            flowGraph.Connect(startNode.OutputFlows[0], logNode.InputFlow);

            flowGraph.Start();

            // Assert
            logNode.IsExecuted.Should().BeTrue();
            sumNode1.IsExecuted.Should().BeFalse();
            sumNode2.IsExecuted.Should().BeFalse();
        }

        [Fact]
        public void Start_ShouldCalculateSum_WhenConnectionChanged()
        {
            // Arrange
            var startNode = new StartNode();
            var valueNode1 = new ValueNode<float>(1.0f);
            var valueNode2 = new ValueNode<float>(2.0f);
            var sumNode = new SumNode("sumNode");
            var logNode = new LogNode<float>("logNode");

            var flowGraph = new FlowGraph("FlowTest")
                .AddNode(startNode)
                .AddNode(valueNode1)
                .AddNode(valueNode2)
                .AddNode(sumNode)
                .AddNode(logNode);

            // Act
            flowGraph.Connect(valueNode1.Output, sumNode.Inputs[0]);
            flowGraph.Connect(valueNode1.Output, sumNode.Inputs[1]);
            flowGraph.Connect(sumNode.Output, logNode.Inputs[0]);

            flowGraph.Connect(startNode.OutputFlows[0], logNode.InputFlow);

            flowGraph.Start();

            flowGraph.Connect(valueNode2.Output, sumNode.Inputs[1]);
            valueNode1.OutputValue = 2.0f;

            flowGraph.Start();

            flowGraph.Disconnect(sumNode.Inputs[0].Connection);

            flowGraph.Start();

            // Assert
            valueNode1.ExecutionCount.Should().Be(2);
            logNode.Values.Count.Should().Be(2);
            logNode.Values[0].Should().Be(2.0f);
            logNode.Values[1].Should().Be(4.0f);
        }
    }
}