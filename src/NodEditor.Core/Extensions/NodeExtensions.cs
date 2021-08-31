using System;
using System.Reflection;
using NodEditor.Core.Attributes;
using NodEditor.Core.Interfaces;

namespace NodEditor.Core.Extensions
{
    public static class NodeExtensions
    {
        public static bool IsStartNode(this INode node)
        {
            return HasCustomAttribute(node, typeof(StartNodeAttribute));
        }
        
        private static bool HasCustomAttribute(INode node, Type type)
        {
            var attribute = node.GetType().GetCustomAttribute(type, true);
            return attribute != null;
        }
    }
}