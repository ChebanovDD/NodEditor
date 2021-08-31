using NodEditor.Core.Interfaces;

namespace NodEditor.Core
{
    public abstract class NodeElement : INodeElement
    {
        private static int _elementId;
        
        public int Id { get; }
        public int ElementIndex { get; set; }
        
        protected NodeElement()
        {
            Id = _elementId++;
        }
    }
}