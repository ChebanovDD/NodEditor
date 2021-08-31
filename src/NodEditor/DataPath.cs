using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class DataPath
    {
        private bool _hasChanges;
        private int _readyNodesCount;
        private List<INode> _nodes = new();

        public DataPath(INode flowNode)
        {
            BuildPath(flowNode.Inputs);
        }

        public bool Execute()
        {
            if (CanExecute() == false)
            {
                return false;
            }
            
            ExecutePath();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanExecute()
        {
            return _readyNodesCount == _nodes.Count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ExecutePath()
        {
            if (_hasChanges == false)
            {
                return;
            }
            
            for (var i = _nodes.Count - 1; i >= 0; i--)
            {
                _nodes[i].Execute();
            }
            
            _hasChanges = false;
        }
        
        private void BuildPath(ReadOnlyArray<IInputSocket> inputs)
        {
            // throw new System.NotImplementedException();
        }
    }
}