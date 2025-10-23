using System.Collections.Generic;
using Base.EventsManager;
using Base.StateMachine.States.Base;
using UnityEngine.InputSystem;

namespace Base.StateMachine.States
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