using System;
using System.IO;
using Newtonsoft.Json;
using SaveSystem.Interfaces;
using UnityEngine;
using Utils.LogSystem;

namespace SaveSystem.Base
{
    public class FileDataStorage<T> : DataStorage<T> where T : IData
    {
        private string _backupPath;

        public FileDataStorage (IConverter<T> converter = null) : base(converter)
        {
            _backupPath = Path.Combine(Application.persistentDataPath, $"{_key}.json");
        }

        public override void Save()
        {
            string stringData = JsonConvert.SerializeObject(Data, GetSettings());
            CL.Log($"Save [{typeof(T).Name}]: {stringData}", Logs.SaveLogs);
            File.WriteAllText(_backupPath, stringData);
        }

        public override void Load()
        {
            _backupPath = Path.Combine(Application.persistentDataPath, $"{_key}.json");

            if (File.Exists(_backupPath))
            {
                string stringData = File.ReadAllText(_backupPath);
                CL.Log($"Load [{typeof(T).Name}]: {stringData}", Logs.SaveLogs);
                Data = JsonConvert.DeserializeObject<T>(stringData, GetSettings());
                HasFile = true;
                return;
            }

            Data = Activator.CreateInstance<T>();
        }

        protected override void ClearData()
        {
            File.Delete(_backupPath);
        }
    }
}