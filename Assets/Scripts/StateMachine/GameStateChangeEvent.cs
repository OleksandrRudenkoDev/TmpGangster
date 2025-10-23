using EventsManager;
using StateMachine.States.Base;

namespace StateMachine
{
    public struct GameStateChangeEvent : IEvent
    {
        public IGameState StateFrom;
        public IGameState StateTo;

        public GameStateChangeEvent(IGameState stateFrom, IGameState stateTo)
        {
            StateFrom = stateFrom;
            StateTo = stateTo;
        }
    }
}