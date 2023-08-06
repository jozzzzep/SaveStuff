using System;
using UnityEngine;
using SavesStuff.Advanced;

namespace SavesStuff
{
    public class SavedInt : SavedVariable<int>
    {
        public SavedInt(string keyName, int deafultValue) :
            base(keyName, deafultValue, TypeCode.Int32, PlayerPrefs.SetInt, PlayerPrefs.GetInt)
        { }
    }
}
