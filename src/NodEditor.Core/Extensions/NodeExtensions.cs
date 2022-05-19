using System.Reflection;
using NodEditor.Core.Interfaces;

namespace NodEditor.Core.Extensions
{
    public static class NodeExtensions
    {
        public static bool HasCustomAttribute<T>(this INode node)
        {
            var attribute = node.GetType().GetCustomAttribute(typeof(T), true);
            return attribute != null;
        }
    }
}