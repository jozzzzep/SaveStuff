using System;
using UnityEngine;

namespace SavesStuff.Advanced
{
    public class SavedVariable<TType>
    {
        public TType Value => cashedValue;

        public string KeyName { get; private set; }
        public TType DeafultValue { get; private set; }
        public TypeCode TypeCode { get; private set; }

        TType cashedValue;
        Action<string, TType> setValueFunction;
        Func<string, TType> getValueFunction;

        public void SetValue(TType value)
        {
            setValueFunction(KeyName, value);
            cashedValue = value;
        }

        TType GetValue()
        {
            TType valueToRetun = cashedValue;

            if (PlayerPrefs.HasKey(KeyName))
                return getValueFunction(KeyName);
            else
            {
                SetValue(DeafultValue);
                return GetValue();
            }
        }

        public SavedVariable(string keyName, TType deafultValue,
            TypeCode typeCode, Action<string, TType> _setValueFunction, Func<string, TType> _getValueFunction)
        {
            KeyName = keyName;
            DeafultValue = deafultValue;
            TypeCode = typeCode;

            setValueFunction = _setValueFunction;
            getValueFunction = _getValueFunction;

            cashedValue = GetValue();
        }
    }
}
