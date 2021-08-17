using System;
using FluentAssertions;
using NodEditor.App.Nodes;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeOutputSocketTests
    {
        private readonly INode _node;

        public NodeOutputSocketTests()
        {
            _node = new DataNode();
        }

        [Fact]
        public void AddOutput_ShouldAddOutputSocket_WhenSocketIsNotNull()
        {
            // Arrange
            var outputSocket = new OutputSocket<float>();
            
            // Act
            _node.AddOutput(outputSocket);
            
            // Assert
            _node.Output.Should().Be(outputSocket);
        }

        [Fact]
        public void AddAndGetOutput_ShouldThrowException_WhenSocketInNull()
        {
            // Act
            Action addNullOutput = () => _node.AddOutput(null);
            Action getOutputValue = () => _node.GetOutputValue<float>();
            
            // Assert
            addNullOutput.Should().Throw<SocketNullReferenceException>();
            getOutputValue.Should().Throw<SocketNullReferenceException>();
        }
        
        [Fact]
        public void GetOutputValue_ShouldReturnInitValue_WhenValueSetFromConstructor()
        {
            // Arrange
            var value = 5.5f;
            var outputSocket = new OutputSocket<float>(value);
            
            // Act
            _node.AddOutput(outputSocket);
            
            // Assert
            _node.GetOutputValue<float>().Should().Be(value);
        }
        
        [Theory]
        [InlineData(0, 5.5f, 5.5f)]
        [InlineData(2.5f, 5.25f, 5.25f)]
        public void GetOutputValue_ShouldReturnValue_WhenValueSet(float initValue, float setValue, float expected)
        {
            // Arrange
            var outputSocket = new OutputSocket<float>(initValue);
            
            // Act
            _node.AddOutput(outputSocket);
            _node.SetOutputValue<float>(setValue);
            
            // Assert
            _node.GetOutputValue<float>().Should().Be(expected);
        }

        [Fact]
        public void SetAndGetOutputValue_ShouldThrowException_WhenTypeIsWrong()
        {
            // Arrange
            var intOutputSocket = new OutputSocket<int>();
            
            // Act
            _node.AddOutput(intOutputSocket);
            Action setFloatOutputValue = () => _node.SetOutputValue(5.5f);
            Action getFloatOutputValue = () => _node.GetOutputValue<float>();
            
            // Assert
            setFloatOutputValue.Should().Throw<InvalidCastException>();
            getFloatOutputValue.Should().Throw<InvalidCastException>();
        }
    }
}