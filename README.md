# NodEditor

> **Warning:** The project is under development.

A cross-platform library that allows you to create your own node-based tools.

<details open><summary><b>FlexNodEditor</b></summary>
<br />

![FlexNodEditor](https://user-images.githubusercontent.com/28132516/138890135-1032c124-49cb-46c0-bb39-e8671482a543.png)

> **FlexNodEditor** is a visual scripting tool for Unity, built on top of **NodEditor** and used in a [StudyBox](https://studybox.rubius.com/) project.
  
</details>

## Table of Contents

- [About](#about)
- [How It Works](#how-it-works)
  - [Flow Graph](#flow-graph)
  - [Entry Node](#entry-node)
  - [Flow Node](#flow-node)
  - [Data Node](#data-node)
- [Made With NodEditor](#made-with-nodeditor)
  - [FlexNodEditor](#flexnodeditor)
- [License](#license)

## About

**NodEditor** helps you create node-based tools for all types of projects.

Build a visual scripting tool that gives you the ability to develop logic for games or applications with visual, drag-and-drop graphs instead of writing code. Enable seamless collaboration between team members for faster prototyping and iteration.

## How It Works

The fundamental idea behind **NodEditor** is to organize nodes into two distinct flows: a logic flow that follows a top-to-bottom composition and reading approach, and a data flow that follows a left-to-right composition and reading approach. This design closely resembles the process of creating flowcharts, making it intuitive for users. We call this flowchart [FlowGraph](#flow-graph).

![Flows](https://github.com/ChebanovDD/NodEditor/assets/28132516/45bc59ec-acd1-476c-8f1f-492020c75d6c)

### Flow Graph

The `FlowGraph` is a visual representation of logic. Each `FlowGraph` has `Start` and `Update` methods that call the `Start` and `Update` entry nodes, respectively.

```csharp
public void FlowGraphTest()
{
    var startNode = new StartNode();
    var valueNode = new ValueNode<string>("Hello World!");
    var logNode = new LogNode();

    var flowGraph = new FlowGraph("FlowTest")
        .AddNode(startNode)
        .AddNode(valueNode)
        .AddNode(logNode);

    flowGraph.Connect(startNode.OutputFlows[0], logNode.InputFlow);
    flowGraph.Connect(valueNode.Output, logNode.Inputs[0]);

    flowGraph.Start();
}
```

### Entry Node

There are two kinds of entry nodes:

- Start – must be marked with the `StartNode` attribute.
- Update – must be marked with the `UpdateNode` attribute.

```csharp
[StartNode]
public class StartNode : FlowNode
{
    private readonly OutputFlowSocket _outputFlow = new();

    public StartNode() : base(nameof(StartNode))
    {
        AddOutputFlows(_outputFlow);
    }

    protected override void OnExecute()
    {
        _outputFlow.Open();
    }
}
```

### Flow Node

```csharp
public class LogNode : FlowNode
{
    private readonly InputSocket<object> _input = new();

    private readonly InputFlowSocket _inputFlow = new();
    private readonly OutputFlowSocket _outputFlow = new();

    public LogNode() : base(nameof(LogNode))
    {
        AddInputs(_input);
        AddInputFlow(_inputFlow);
        AddOutputFlows(_outputFlow);
    }

    protected override void OnExecute(bool allDataPathsExecuted)
    {
        if (allDataPathsExecuted)
        {
            Console.WriteLine(_input.Value);
        }

        _outputFlow.Open();
    }
}
```

### Data Node

```csharp
public class SumNode : DataNode
{
    private readonly InputSocket<float> _input1 = new();
    private readonly InputSocket<float> _input2 = new();
    private readonly OutputSocket<float> _output = new();

    public SumNode() : base(nameof(SumNode))
    {
        AddInputs(_input1, _input2);
        AddOutput(_output);
    }

    protected override void OnExecute()
    {
        _output.Value = _input1.Value + _input2.Value;
    }
}
```

## Made With NodEditor

### FlexNodEditor

https://github.com/ChebanovDD/NodEditor/assets/28132516/a3008518-3856-4e70-9180-ea541c541116

## License

Usage is provided under the [MIT License](LICENSE).
