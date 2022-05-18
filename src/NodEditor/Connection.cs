using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class Connection : IConnection
    {
        private bool? _isCompatible;

        public Connection(IOutputSocket output, IInputSocket input)
        {
            Output = output;
            Input = input;
        }

        public IInputSocket Input { get; }
        public IOutputSocket Output { get; }

        public bool IsCompatible
        {
            get
            {
                if (_isCompatible.HasValue)
                {
                    return _isCompatible.Value;
                }

                _isCompatible = IsTypesCompatible(Output, Input) &&
                                IsLoopedConnection(Output, Input) == false;

                return _isCompatible.Value;
            }
        }

        public void Remove()
        {
            Input.RemoveConnection(this);
            Output.RemoveConnection(this);
        }

        private static bool IsTypesCompatible(IOutputSocket output, IInputSocket input)
        {
            return output.Type == input.Type || input.Type == typeof(object);
        }

        private static bool IsLoopedConnection(IOutputSocket output, IInputSocket input)
        {
            var outputSocketNode = output.Node;
            if (outputSocketNode == null || outputSocketNode.HasInputs == false)
            {
                return false;
            }

            for (var i = 0; i < outputSocketNode.Inputs.Length; i++)
            {
                var inputSocket = outputSocketNode.Inputs[i];
                if (inputSocket.HasConnections == false)
                {
                    continue;
                }

                if (inputSocket.Connection.Input.Node.Guid == input.Node.Guid ||
                    inputSocket.Connection.Output.Node.Guid == input.Node.Guid)
                {
                    return true;
                }
            }

            return false;
        }
    }
}