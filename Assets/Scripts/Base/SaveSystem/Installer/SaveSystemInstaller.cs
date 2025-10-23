using Base.SaveSystem.Base;
using Base.SaveSystem.Converters;
using Base.SaveSystem.Data;
using Base.SaveSystem.Interfaces;
using Zenject;

namespace Base.SaveSystem.Installer
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