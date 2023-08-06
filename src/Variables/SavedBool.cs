using System;
using UnityEngine;
using SavesStuff.Advanced;

namespace SavesStuff
{
    public class SavedBool : SavedVariable<bool>
    {
        public SavedBool(string keyName, bool deafultValue) :
            base(keyName, deafultValue, TypeCode.Boolean, SetBool, GetBool)
        { }

        private static void SetBool(string keyName, bool value) => PlayerPrefs.SetInt(keyName, value ? 1 : 0);
        private static bool GetBool(string keyName) => PlayerPrefs.GetInt(keyName) == 1 ? true : false;
    }
}
