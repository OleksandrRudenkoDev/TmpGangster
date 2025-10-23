
namespace StateMachine.States.Base
{
    public interface IGameState : IState
    {
        bool CanEnterFrom (IState currentState);
    }
}