using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class OutputSocket<TValue> : IOutputSocket
    {
        private TValue _value;

        public OutputSocket()
        {
        }

        public OutputSocket(TValue value)
        {
            _value = value;
        }

        public TValue GetValue()
        {
            return _value;
        }

        public void SetValue(TValue value)
        {
            _value = value;
        }
    }
}