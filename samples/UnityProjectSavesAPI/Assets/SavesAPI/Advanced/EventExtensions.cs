using System.Collections.Generic;
using System;

namespace SavesAPI.Advanced
{
    internal static class EventExtensions
    {
        internal static void SafeInvoke<T>(this Action<T> action, T args)
            where T : class, ISaveable
        {
            if (action != null)
                action(args);
        }

        internal static void SafeInvoke<T>(this Action<List<T>> action, List<T> args)
            where T : class, ISaveable
        {
            if (action != null)
                action(args);
        }

        internal static void SafeInvoke(this Action<List<string>> action, List<string> args)
        {
            if (action != null)
                action(args);
        }

        internal static void SafeInvoke(this Action<string> action, string args)
        {
            if (action != null)
                action(args);
        }

        internal static void SafeInvoke(this Action<SaveSlot> action, SaveSlot args)
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
