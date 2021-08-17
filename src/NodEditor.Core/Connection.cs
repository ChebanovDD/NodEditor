﻿using NodEditor.Core.Interfaces;

namespace NodEditor.Core
{
    public class Connection
    {
        public bool IsCompatible { get; }
        public IInputSocket Input { get; }
        public IOutputSocket Output { get; }

        public Connection(IOutputSocket output, IInputSocket input)
        {
            Output = output;
            Input = input;
            IsCompatible = output.Type == input.Type;
        }

        public void Remove()
        {
            Input.RemoveConnection(this);
            Output.RemoveConnection(this);
        }
    }
}