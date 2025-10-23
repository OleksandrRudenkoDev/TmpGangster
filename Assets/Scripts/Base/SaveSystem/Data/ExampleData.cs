using System;
using System.Collections.Generic;
using SaveSystem.Interfaces;
using SaveSystem.Other;

namespace SaveSystem.Data
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