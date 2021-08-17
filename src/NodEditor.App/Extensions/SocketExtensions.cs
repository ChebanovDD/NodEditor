using System.Runtime.CompilerServices;
using NodEditor.App.Sockets;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Extensions
{
    public static class SocketExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IOutputSocket output)
        {
            return ((OutputSocket<T>)output).GetValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<T>(this IOutputSocket output, T value)
        {
            ((OutputSocket<T>)output).SetValue(value);
        }
    }
}