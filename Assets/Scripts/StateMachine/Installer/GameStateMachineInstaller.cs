using StateMachine.States;
using StateMachine.States.Base;
using Zenject;

namespace StateMachine.Installer
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInputActions>().AsSingle().NonLazy();
            
            Container.Bind<IGameState>().To<ExampleState>().AsSingle();
            Container.Bind<IGameState>().To<DefaultState>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }
    }
}