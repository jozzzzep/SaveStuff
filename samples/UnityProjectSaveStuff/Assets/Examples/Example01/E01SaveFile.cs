using SavesStuff;
using System;
using UnityEngine.Experimental.GlobalIllumination;

namespace Examples.E01
{
    [Serializable]
    public class E01SaveFile : ISaveable
    {
        public static string StaticName = "TextContentMenu";

        public string Name => StaticName;

        public DateTime LastUsage { get; set; }

        public string TextContent { get; private set; }

        public E01SaveFile(string textContext)
        {
            TextContent = textContext;
        }
    }
}
