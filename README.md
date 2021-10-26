# NodEditor

A cross-platform library that allows you to create your own node-based tools.

<details open><summary><b>FlexNodEditor</b></summary>
<br />

![FlexNodEditor](https://user-images.githubusercontent.com/28132516/138890135-1032c124-49cb-46c0-bb39-e8671482a543.png)

> **FlexNodEditor** is a visual scripting tool for Unity, built on top of **NodEditor** and used in a [StudyBox](https://studybox.rubius.com/) project.
  
</details>

## Table of Contents

- [About](#about)
- [How It Works](#how-it-works)
- [Examples](#examples)
- [How To Use](#how-to-use)
- [License](#license)

## About

NodEditor helps you create node-based tools for all types of projects.

Build a visual scripting tool that gives you the ability to develop logic for games or applications with visual, drag-and-drop graphs instead of writing a code. Enable seamless collaboration between team members for faster prototyping and iteration.

## How It Works

...

### Flow Node

...

### DataNode

...

## Examples

### Entry Node

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

### Sum Node

```csharp
public class SumNode : DataNode
{
    private readonly InputSocket<float> _input1 = new();
    private readonly InputSocket<float> _input2 = new();
    private readonly OutputSocket<float> _output = new();

    public SumNode(string name) : base(name)
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

## How To Use

...

## License

Usage is provided under the [MIT License](LICENSE).
