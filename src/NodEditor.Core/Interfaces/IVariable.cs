using System;

namespace NodEditor.Core.Interfaces
{
    public interface IVariable
    {
        Guid Guid { get; }
        string Name { get; }
    }
}