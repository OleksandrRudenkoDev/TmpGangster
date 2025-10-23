using Zenject;

namespace EventsManager
{
    public class EventManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventManager>().AsSingle();
        }
    }
}