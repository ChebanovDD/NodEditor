﻿using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.App.Sockets;
using NodEditor.UnitTests.Nodes;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeExecuteTests
    {
        private readonly SumNode _sumNode;
        private readonly INodeEditor _nodeEditor;

        public NodeExecuteTests()
        {
            _sumNode = new SumNode();
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }

        [Theory]
        [InlineData(0.0f, 0.0f, 0.0f)]
        [InlineData(0.25f, 0.75f, 1.0f)]
        [InlineData(-0.25f, 0.75f, 0.5f)]
        public void Execute_ShouldCalculateSum_WhenInputsAreValid(float value1, float value2, float expectation)
        {
            // Arrange
            var outputSocket1 = new OutputSocket<float> { Value = value1 };
            var outputSocket2 = new OutputSocket<float> { Value = value2 };
            
            // Act
            _nodeEditor.Connect(outputSocket1, _sumNode.Inputs[0]);
            _nodeEditor.Connect(outputSocket2, _sumNode.Inputs[1]);
            
            outputSocket1.UpdateLastInputValue();
            outputSocket2.UpdateLastInputValue();
            
            _sumNode.Execute();
            
            // Assert
            _sumNode.GetOutputValue<float>().Should().Be(expectation);
        }
    }
}