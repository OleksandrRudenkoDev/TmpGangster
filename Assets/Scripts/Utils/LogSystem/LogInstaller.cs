using UnityEngine;
using Zenject;

namespace Utils.LogSystem
{
    public class LogInstaller : MonoInstaller
    {
        [SerializeField]
        private CL _prefab;

        public override void InstallBindings()
        {
            Container.Bind<CL>().FromComponentInNewPrefab(_prefab).AsSingle().NonLazy();
        }
    }
}