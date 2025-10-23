using System.Collections.Generic;
using EventsManager;
using StateMachine.States.Base;
using UnityEngine.InputSystem;

namespace StateMachine.States
{
    public class ExampleState : BaseGameState
    {
        public ExampleState(PlayerInputActions actions, EventManager eventManager) : base(actions, eventManager)
        {}

        protected override void InitMaps()
        {
            _activeMaps = new List<InputActionMap>();
        }
    }
}