using System;
using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.UnitTests.DataNodes;
using NodEditor.UnitTests.FlowNodes;
using NodEditor.Variables;
using Xunit;

namespace NodEditor.UnitTests
{
    public class VariableTests
    {
        private readonly INodeEditor _nodeEditor;

        public VariableTests()
        {
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }
        
        [Fact]
        public void Start_ShouldExecuteFlow_WhenFlowIsValid()
        {
            // Arrange
            var startNode = new StartNode();
            var valueNode = new ValueNode<float>(2.5f);
            var logNode = new LogNode<float>("logNode");
            var speedVariable = new Variable<float>(Guid.NewGuid(), "Speed", 5.5f);

            var flowGraph = new FlowGraph("VariableFlowTest")
                .RegisterVariable(speedVariable);

            var getSpeedNode = flowGraph.CreateGetVariableNode<float>(speedVariable.Guid);
            var setSpeedNode = flowGraph.CreateSetVariableNode<float>(speedVariable.Guid);

            flowGraph
                .AddNode(startNode)
                .AddNode(valueNode)
                .AddNode(logNode)
                .AddNode(getSpeedNode)
                .AddNode(setSpeedNode);
            
            // Act
            _nodeEditor.Connect(getSpeedNode.Output, logNode.Inputs[0]);
            _nodeEditor.Connect(startNode.OutputFlows[0], setSpeedNode.InputFlow);
            _nodeEditor.Connect(setSpeedNode.OutputFlows[0], logNode.InputFlow);
            
            flowGraph.Start();

            var valueBeforeConnection = logNode.GetLastValue();
            
            _nodeEditor.Connect(valueNode.Output, setSpeedNode.Inputs[0]);
            
            flowGraph.Start();
            
            // Assert
            valueBeforeConnection.Should().Be(5.5f);
            logNode.GetLastValue().Should().Be(2.5f);
        }
    }
}