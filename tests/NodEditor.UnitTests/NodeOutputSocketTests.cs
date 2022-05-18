using System;
using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.UnitTests.DataNodes;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeOutputSocketTests
    {
        private readonly MockDataNode _dataNode;
        private readonly INodeEditor _nodeEditor;
        
        public NodeOutputSocketTests()
        {
            _dataNode = new MockDataNode();
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }
        
        [Fact]
        public void AddOutput_ShouldAddOutput_WhenOutputIsValid()
        {
            // Arrange
            var outputSocket = new OutputSocket<float>();
            
            // Act
            _dataNode.AddOutputTest(outputSocket);
            
            // Assert
            _dataNode.HasOutput.Should().BeTrue();
            _dataNode.Output.Should().Be(outputSocket);
        }
        
        [Fact]
        public void AddOutput_ShouldThrowException_WhenOutputIsNull()
        {
            // Act
            Action addNullOutput = () => _dataNode.AddOutputTest(null);
            
            // Assert
            addNullOutput.Should().Throw<SocketCanNotBeNullReferenceException>();
        }
        
        [Fact]
        public void GetOutputValue_ShouldReturnValue_WhenTypeIsCorrect()
        {
            // Arrange
            var outValue = 5;
            var anyInputSocket = new InputSocket<int>();
            var outputSocket = new OutputSocket<int>(5);

            // Act
            _dataNode.AddOutputTest(outputSocket);
            _nodeEditor.Connect(outputSocket, anyInputSocket);
            
            // Assert
            _dataNode.GetOutputValue<int>().Should().Be(outValue);
        }
        
        [Fact]
        public void GetOutputValue_ShouldThrowException_WhenTypeIsWrong()
        {
            // Arrange
            var outputSocket = new OutputSocket<int>();
            
            // Act
            _dataNode.AddOutputTest(outputSocket);
            
            Action getFloatOutputValue = () => _dataNode.GetOutputValue<float>();
            
            // Assert
            getFloatOutputValue.Should().Throw<InvalidCastException>();
        }
        
        [Fact]
        public void GetOutputValue_ShouldThrowException_WhenOutputIsNull()
        {
            // Act
            Action getOutputValue = () => _dataNode.GetOutputValue<float>();
            
            // Assert
            getOutputValue.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void Disconnect_ShouldResetValue_WhenDisconnected()
        {
            // Arrange
            var anyInputSocket = new InputSocket<int>();
            var outputSocket = new OutputSocket<int>(5);

            // Act
            _dataNode.AddOutputTest(outputSocket);
            
            var connection = _nodeEditor.Connect(outputSocket, anyInputSocket);
            _nodeEditor.Disconnect(connection);
            
            // Assert
            _dataNode.GetOutputValue<int>().Should().Be(default);
        }
    }
}