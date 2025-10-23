using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using SaveSystem.Interfaces;
using SaveSystem.Other;

namespace SaveSystem.Base
{
    public abstract class DataStorage<T> : IDataStorage
        where T : IData
    {
        protected readonly IConverter<T> _converter;
        protected string _key;

        public DataStorage(IConverter<T> converter = null)
        {
            _converter = converter;
            GetKey();
            Load();
        }

        public abstract void Save();

        public abstract void Load();

        public void Clear()
        {
            HasFile = false;
            ClearData();
            Data = Activator.CreateInstance<T>();
        }

        protected abstract void ClearData();

        protected JsonSerializerSettings GetSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            if (_converter != null)
            {
                settings.Converters = new List<JsonConverter>
                {
                    _converter.Converter
                };
            }

            return settings;
        }

        protected void GetKey()
        {
            DataStorageKeyAttribute attr = typeof(T).GetCustomAttribute<DataStorageKeyAttribute>();

            if (attr == null || string.IsNullOrWhiteSpace(attr.Key))
            {
                throw new InvalidOperationException($"{typeof(T).Name} must have [DataStorageKeyAttribute] with a non-empty key");
            }

            _key = attr.Key;
        }

        public bool HasFile
        {
            get;
            protected set;
        }

        public T Data
        {
            get;
            set;
        }
    }
}