using SaveSystem.Base;
using SaveSystem.Converters;
using SaveSystem.Data;
using SaveSystem.Interfaces;
using Zenject;

namespace SaveSystem.Installer
{
    public class SaveSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallConverters();
            InstallData();
        }

        private void InstallConverters()
        {
            Container.Bind<IConverter<ExampleData>>().To<ExampleConverter>().AsTransient();
        }

        private void InstallData()
        {
#if UNITY_EDITOR
            Container.Bind<DataStorage<ExampleData>>().To<FileDataStorage<ExampleData>>().AsSingle();
#else
            Container.Bind<DataStorage<ExampleData>>().To<PlayerPrefsDataStorage<ExampleData>>().AsSingle();
#endif
        }
    }
}