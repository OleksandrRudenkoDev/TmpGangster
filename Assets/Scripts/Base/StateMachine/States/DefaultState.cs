using System.Collections.Generic;
using Base.EventsManager;
using Base.StateMachine.States.Base;
using UnityEngine.InputSystem;

namespace Base.StateMachine.States
{
    public class DefaultState : BaseGameState
    {
        public DefaultState (PlayerInputActions actions, EventManager eventManager) : base(actions, eventManager)
        {
        }

        public override bool CanEnterFrom (IState currentState)
        {
            return true;
        }

        protected override void InitMaps()
        {
            _activeMaps = new List<InputActionMap>()
            {
                _actions.Move,
                _actions.Camera
            };
        }
    }
}