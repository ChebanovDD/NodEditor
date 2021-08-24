using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.App.Sockets;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeConnectionTests
    {
        private readonly INodeEditor _nodeEditor;
        
        public NodeConnectionTests()
        {
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }

        [Fact]
        public void Connect_ShouldConnectSockets_WhenTypesMatch()
        {
            // Arrange
            var inputSocket1 = new InputSocket<int>();
            var inputSocket2 = new InputSocket<int>();
            var outputSocket = new OutputSocket<int>();

            // Act
            var connection1 = _nodeEditor.Connect(outputSocket, inputSocket1);
            var connection2 = _nodeEditor.Connect(outputSocket, inputSocket2);

            // Assert
            connection1.IsCompatible.Should().BeTrue();
            connection2.IsCompatible.Should().BeTrue();

            connection1.Input.Should().Be(inputSocket1);
            connection1.Output.Should().Be(outputSocket);
            
            connection2.Input.Should().Be(inputSocket2);
            connection2.Output.Should().Be(outputSocket);
            
            inputSocket1.HasConnections.Should().BeTrue();
            outputSocket.HasConnections.Should().BeTrue();
            outputSocket.Connections.Count.Should().Be(2);
            
            inputSocket1.Connection.Should().Be(connection1);
            inputSocket2.Connection.Should().Be(connection2);
            outputSocket.Connections[0].Should().Be(connection1);
            outputSocket.Connections[1].Should().Be(connection2);
            
            inputSocket1.Connection.Input.Should().Be(inputSocket1);
            inputSocket1.Connection.Output.Should().Be(outputSocket);
            inputSocket2.Connection.Input.Should().Be(inputSocket2);
            inputSocket2.Connection.Output.Should().Be(outputSocket);
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

        [Fact]
        public void Connect_ShouldDisconnectPreviousConnection_WhenInputHasConnection()
        {
            // Arrange
            var inputSocket = new InputSocket<int>();
            var outputSocket1 = new OutputSocket<int>();
            var outputSocket2 = new OutputSocket<int>();
            
            // Act
            _nodeEditor.Connect(outputSocket1, inputSocket);
            var newConnection = _nodeEditor.Connect(outputSocket2, inputSocket);

            // Assert
            inputSocket.HasConnections.Should().BeTrue();
            outputSocket1.HasConnections.Should().BeFalse();
            outputSocket2.HasConnections.Should().BeTrue();
            
            inputSocket.Connection.Should().Be(newConnection);
            outputSocket2.Connections[0].Should().Be(newConnection);
        }

        [Fact]
        public void Disconnect_ShouldDisconnectSockets_WhenHasConnection()
        {
            // Arrange
            var inputSocket = new InputSocket<int>();
            var outputSocket = new OutputSocket<int>(5);
            
            // Act
            var connection = _nodeEditor.Connect(outputSocket, inputSocket);
            _nodeEditor.Disconnect(connection);
            
            // Assert
            inputSocket.HasConnections.Should().BeFalse();
            outputSocket.HasConnections.Should().BeFalse();
        }
    }
}