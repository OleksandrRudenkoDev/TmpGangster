using System.Collections.Generic;
using Base.EventsManager;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Base.StateMachine.States.Base
{
    public abstract class BaseGameState : IGameState
    {
        protected readonly PlayerInputActions _actions;
        protected readonly EventManager _eventManager;

        protected List<InputActionMap> _activeMaps = new List<InputActionMap>();

        public BaseGameState(PlayerInputActions actions, EventManager eventManager)
        {
            _actions = actions;
            _eventManager = eventManager;
        }

        public virtual void EnterState()
        {
            InitMaps();
            ActivateMaps();
        }

        public virtual void ExitState()
        {}

        protected void ActivateMaps()
        {
            ReadOnlyArray<InputActionMap> maps = _actions.asset.actionMaps;

            foreach(InputActionMap map in maps)
            {
                if(_activeMaps.Contains(map))
                {
                    map.Enable();
                } else
                {
                    map.Disable();
                }
            }
        }

        public virtual bool CanEnterFrom(IState currentState)
        {
            return currentState is DefaultState;
        }

        protected abstract void InitMaps();
    }
}