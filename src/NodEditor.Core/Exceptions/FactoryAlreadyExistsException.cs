using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.Core.Exceptions
{
    public class FactoryAlreadyExistsException : Exception
    {
        public FactoryAlreadyExistsException(INodeFactory factory)
            : base($"Factory {factory.Name} already exists.")
        {
        }
    }
}