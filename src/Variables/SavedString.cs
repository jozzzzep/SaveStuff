using System;
using UnityEngine;
using SavesStuff.Advanced;

namespace SavesStuff
{
    public class SavedString : SavedVariable<string>
    {
        public SavedString(string keyName, string deafultValue) :
            base(keyName, deafultValue, TypeCode.String, PlayerPrefs.SetString, PlayerPrefs.GetString)
        { }
    }
}
