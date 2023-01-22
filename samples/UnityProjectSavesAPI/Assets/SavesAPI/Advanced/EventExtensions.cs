using System.Collections.Generic;
using System;

namespace SavesAPI.Advanced
{
    internal static class EventExtensions
    {
        internal static void SafeInvoke(this Action action)
        {
            if (action != null)
                action();
        }

        internal static void SafeInvoke<T>(this Action<T> action, T args)
            where T : class, ISaveable
        {
            if (action != null)
                action(args);
        }

        internal static void SafeInvoke(this Action<string> action, string args)
        {
            if (action != null)
                action(args);
        }

        internal static void SafeInvoke(this Action<int> action, int args)
        {
            if (action != null)
                action(args);
        }


        internal static void SafeInvoke<T>(this Action<T[]> action, T[] args)
            where T : class, ISaveable
        {
            if (action != null)
                action(args);
        }
    }
}
