using AutoFixture;
using FluentAssertions;
using NodEditor.App.Interfaces;
using NodEditor.App.Sockets;
using NodEditor.UnitTests.Nodes;
using Xunit;

namespace NodEditor.UnitTests
{
    public class NodeExecuteTests
    {
        private readonly IFixture _fixture;
        private readonly TestSumNode _sumNode;
        private readonly INodeEditor _nodeEditor;

        public NodeExecuteTests()
        {
            _fixture = new Fixture();
            _sumNode = new TestSumNode();
            _nodeEditor = new NodeEditor(new FlowManager(), new Connector());
        }

        [Theory]
        [InlineData(0.0f, 0.0f, 0.0f)]
        [InlineData(0.25f, 0.75f, 1.0f)]
        [InlineData(-0.25f, 0.75f, 0.5f)]
        public void Execute_ShouldCalculateSum_WhenInputsAreValid(float value1, float value2, float expectation)
        {
            // Arrange
            var outputSocket1 = _fixture
                .Build<OutputSocket<float>>()
                .With(output => output.Value, value1)
                .Create();
            var outputSocket2 = _fixture
                .Build<OutputSocket<float>>()
                .With(output => output.Value, value2)
                .Create();
            
            // Act
            _nodeEditor.Connect(outputSocket1, _sumNode.Inputs[0]);
            _nodeEditor.Connect(outputSocket2, _sumNode.Inputs[1]);
            _sumNode.Execute();
            
            // Assert
            _sumNode.GetOutputValue<float>().Should().Be(expectation);
        }
    }
}