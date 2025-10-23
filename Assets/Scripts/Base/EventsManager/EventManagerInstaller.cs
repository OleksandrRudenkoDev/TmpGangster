using Zenject;

namespace Base.EventsManager
{
    public class EventManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventManager>().AsSingle();
        }
    }
}