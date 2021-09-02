using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.Variables
{
    public class Variable<TValue> : IVariable
    {
        public Guid Guid { get; }
        public string Name { get; }
        public TValue Value { get; internal set; }

        public Variable(Guid guid, string name, TValue value = default)
        {
            Guid = guid;
            Name = name;
            Value = value;
        }
    }
}