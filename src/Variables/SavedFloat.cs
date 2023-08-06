using System;
using UnityEngine;
using SavesStuff.Advanced;

namespace SavesStuff
{
    public class SavedFloat : SavedVariable<float>
    {
        public SavedFloat(string keyName, float deafultValue) :
            base(keyName, deafultValue, TypeCode.Single, PlayerPrefs.SetFloat, PlayerPrefs.GetFloat)
        { }
    }
}
