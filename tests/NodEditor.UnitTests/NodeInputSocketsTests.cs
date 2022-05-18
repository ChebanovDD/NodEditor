using System;
using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.UnitTests.Nodes;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeInputSocketsTests
    {
        private readonly MockDataNode _dataNode;
        private readonly INodeEditor _nodeEditor;
        
        public NodeInputSocketsTests()
        {
            _dataNode = new MockDataNode();
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }
        
        [Fact]
        public void AddInputs_ShouldAddInputs_WhenInputsAreValid()
        {
            // Arrange
            var inputSocket1 = new InputSocket<float>();
            var inputSocket2 = new InputSocket<float>();
            
            // Act
            _dataNode.AddInputsTest(inputSocket1, inputSocket2);
            
            // Assert
            _dataNode.HasInputs.Should().BeTrue();
            _dataNode.Inputs[0].Should().Be(inputSocket1);
            _dataNode.Inputs[1].Should().Be(inputSocket2);
        }
        
        [Fact]
        public void AddInputs_ShouldThrowException_WhenInputsAreNull()
        {
            // Act
            Action addNullInput = () => _dataNode.AddInputsTest(null);
            
            // Assert
            addNullInput.Should().Throw<SocketCanNotBeNullReferenceException>();
        }
        
        [Fact]
        public void GetInputValue_ShouldReturnValue_WhenTypeIsCorrect()
        {
            // Arrange
            var outValue = 5;
            var inputSocket = new InputSocket<int>();
            var anyOutputSocket = new OutputSocket<int>(outValue);

            // Act
            _dataNode.AddInputsTest(inputSocket);
            _nodeEditor.Connect(anyOutputSocket, inputSocket);

            anyOutputSocket.Value = outValue;
            
            // Assert
            _dataNode.GetInputValue<int>(0).Should().Be(outValue);
        }
        
        [Fact]
        public void GetInputValue_ShouldThrowException_WhenTypeIsWrong()
        {
            // Arrange
            var inputSocket = new InputSocket<int>();
            
            // Act
            _dataNode.AddInputsTest(inputSocket);
            
            Action getFloatInputValue = () => _dataNode.GetInputValue<float>(0);
            
            // Assert
            getFloatInputValue.Should().Throw<InvalidCastException>();
        }
        
        [Fact]
        public void GetInputValue_ShouldThrowException_WhenInputsAreNull()
        {
            // Act
            Action getInputValue = () => _dataNode.GetInputValue<float>(0);
            
            // Assert
            getInputValue.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void Disconnect_ShouldResetValue_WhenDisconnected()
        {
            // Arrange
            var inputSocket = new InputSocket<int>();
            var anyOutputSocket = new OutputSocket<int>(5);

            // Act
            _dataNode.AddInputsTest(inputSocket);
            
            _nodeEditor.Connect(anyOutputSocket, inputSocket);
            _nodeEditor.Disconnect(inputSocket.Connection);
            
            // Assert
            _dataNode.GetInputValue<int>(0).Should().Be(default);
        }
    }
}