using System;

namespace Base.SaveSystem.Other
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DataStorageKeyAttribute : Attribute
    {

        public DataStorageKeyAttribute (string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
}