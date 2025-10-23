using System;
using System.Collections.Generic;
using Base.SaveSystem.Interfaces;
using Base.SaveSystem.Other;

namespace Base.SaveSystem.Data
{
    [Serializable, DataStorageKey("ExapleData")]
    public class ExampleData : IData
    {
        public List<ExampleDataItem> ExampleDataItems = new List<ExampleDataItem>();
    }

    [Serializable]
    public class ExampleDataItem
    {
        public int IntExample;
        public bool BoolExample;
        public string StringExample;
    }
}