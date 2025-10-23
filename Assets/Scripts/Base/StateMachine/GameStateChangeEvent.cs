using Base.EventsManager;
using Base.StateMachine.States.Base;

namespace Base.StateMachine
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