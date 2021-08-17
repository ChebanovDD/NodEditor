using System;
using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.App.Nodes;
using NodEditor.App.Sockets;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;
using NSubstitute;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeInputSocketsTests
    {
        private readonly INode _node;
        private readonly INodeEditor _nodeEditor;
        
        private readonly IFlowManager _flowManager = Substitute.For<IFlowManager>();

        public NodeInputSocketsTests()
        {
            _node = new DataNode();
            _nodeEditor = new NodeEditor(_flowManager);
        }
        
        [Fact]
        public void AddInput_ShouldAddInputSocket_WhenSocketIsNotNull()
        {
            // Arrange
            var inputSocket1 = new InputSocket<float>();
            var inputSocket2 = new InputSocket<float>();
            
            // Act
            _node.AddInput(inputSocket1);
            _node.AddInput(inputSocket2);
            
            // Assert
            _node.HasInputs.Should().BeTrue();
            _node.Inputs[0].Should().Be(inputSocket1);
            _node.Inputs[1].Should().Be(inputSocket2);
        }
        
        [Fact]
        public void AddAndGetInput_ShouldThrowException_WhenSocketInNull()
        {
            // Act
            Action addNullInput = () => _node.AddInput(null);
            Action getInputValue = () => _node.GetInputValue<float>(0);
            
            // Assert
            addNullInput.Should().Throw<SocketNullReferenceException>();
            getInputValue.Should().Throw<SocketNullReferenceException>();
        }

        [Fact]
        public void GetInputValue_ShouldReturnValueFromConnection_WhenTypesIsMatch()
        {
            // Arrange
            const int initValue = 5;
            const int setValue = 10;
            
            var inputSocket = new InputSocket<int>();
            var outputSocket = new OutputSocket<int>(initValue);
            outputSocket.SetValue(setValue);
            
            // Act
            _node.AddInput(inputSocket);
            _nodeEditor.Connect(outputSocket, inputSocket);
            
            // Assert
            inputSocket.GetValue().Should().Be(setValue);
            _node.GetInputValue<int>(0).Should().Be(setValue);
        }
        
        [Fact]
        public void GetInputValue_ShouldThrowException_WhenTypeIsWrong()
        {
            // Arrange
            var intInputSocket = new InputSocket<int>();
            
            // Act
            _node.AddInput(intInputSocket);
            Action getFloatInputValue = () => _node.GetInputValue<float>(0);
            
            // Assert
            getFloatInputValue.Should().Throw<InvalidCastException>();
        }
    }
}