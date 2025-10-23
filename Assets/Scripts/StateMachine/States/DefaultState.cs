using System.Collections.Generic;
using EventsManager;
using StateMachine.States.Base;
using UnityEngine.InputSystem;

namespace StateMachine.States
{
    public class DefaultState : BaseGameState
    {
        public DefaultState(PlayerInputActions actions, EventManager eventManager) : base(actions, eventManager)
        {}

        protected override void InitMaps()
        {
            _activeMaps = new List<InputActionMap>();
        }
    }
}