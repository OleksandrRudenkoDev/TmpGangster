using System;
using Newtonsoft.Json;
using SaveSystem.Interfaces;
using UnityEngine;
using Utils.LogSystem;

namespace SaveSystem.Base
{
    public class PlayerPrefsDataStorage<T> : DataStorage<T> where T : IData
    {
        public override void Save()
        {
            string stringData = JsonConvert.SerializeObject(Data, GetSettings());

            CL.Log($"Save [{typeof(T).Name}]: {stringData}", Logs.SaveLogs);

            PlayerPrefs.SetString(_key, stringData);
            HasFile = PlayerPrefs.HasKey(_key);
        }

        public override void Load()
        {
            HasFile = PlayerPrefs.HasKey(_key);

            if (HasFile)
            {
                string stringData = PlayerPrefs.GetString(_key, "");

                CL.Log($"Load [{typeof(T).Name}]: {stringData}", Logs.SaveLogs);

                Data = JsonConvert.DeserializeObject<T>(stringData, GetSettings());

                return;
            }

            Data = Activator.CreateInstance<T>();
        }

        protected override void ClearData()
        {
            PlayerPrefs.DeleteKey(_key);
        }
    }
}