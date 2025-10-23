using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Base.EventsManager;
using Base.SaveSystem.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Base.SaveSystem.Base
{
    public class SaveManager : IInitializable, IDisposable
    {
        private readonly EventManager _eventManager;
        private readonly List<IDataStorage> _storages;
        private CancellationTokenSource _cts;

        public SaveManager (EventManager eventManager, IEnumerable<IDataStorage> storages)
        {
            _eventManager = eventManager;
            _storages = storages.ToList();
        }

        public void Dispose()
        {
            _eventManager.Unsubscribe<SaveEvent>(HandleSave);
            _cts?.DisposeSource();
        }

        public void Initialize()
        {
            _eventManager.Subscribe<SaveEvent>(HandleSave);
            _cts = new CancellationTokenSource();
            StartLoop(_cts.Token).Forget();
        }

        private async UniTaskVoid StartLoop (CancellationToken token)
        {
            do
            {
                await UniTask.WaitForSeconds(30, cancellationToken: token);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                PlayerPrefs.Save();
            } while (!token.IsCancellationRequested);
        }

        public void ClearAll()
        {
            PlayerPrefs.DeleteAll();

            foreach (IDataStorage storage in _storages)
            {
                storage.Clear();
            }

            PlayerPrefs.Save();
        }

        private void HandleSave (SaveEvent eventData)
        {
            foreach (IDataStorage storage in _storages)
            {
                storage.Save();
            }

            PlayerPrefs.Save();
        }
    }
}