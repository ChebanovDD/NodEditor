using FluentAssertions;
using NodEditor.App;
using NodEditor.App.Sockets;
using NodEditor.Core.Interfaces;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeConnectionTests
    {
        private readonly INodeEditor _nodeEditor;

        public NodeConnectionTests()
        {
            _nodeEditor = new NodeEditor();
        }

        [Fact]
        public void Connect_ShouldConnectSockets_WhenTypesMatch()
        {
            // Arrange
            const int value = 5;
            var inputSocket = new InputSocket<int>();
            var outputSocket = new OutputSocket<int>(value);

            // Act
            var connection = _nodeEditor.Connect(outputSocket, inputSocket);

            // Assert
            connection.IsCompatible.Should().BeTrue();
            
            inputSocket.HasConnections.Should().BeTrue();
            outputSocket.HasConnections.Should().BeTrue();

            inputSocket.Connection.Should().Be(connection);
            outputSocket.Connections[0].Should().Be(connection);
            
            inputSocket.Connection.Input.Should().Be(inputSocket);
            inputSocket.Connection.Output.Should().Be(outputSocket);

            inputSocket.GetValue().Should().Be(value);
        }

        [Fact]
        public void Connect_ShouldBeIncompatible_WhenTypesMismatch()
        {
            // Arrange
            var inputSocket = new InputSocket<int>();
            var outputSocket = new OutputSocket<float>();

            // Act
            var connection = _nodeEditor.Connect(outputSocket, inputSocket);

            // Assert
            connection.IsCompatible.Should().BeFalse();
        }
    }
}