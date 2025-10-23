using System;
using System.Collections.Generic;
using System.Linq;
using EventsManager;
using StateMachine.States;
using StateMachine.States.Base;
using UnityEngine;
using Utils.LogSystem;
using Zenject;

namespace StateMachine
{
    public class GameStateMachine : IInitializable
    {
        private readonly Stack<IGameState> _stateStack = new Stack<IGameState>();
        private readonly Dictionary<Type, IGameState> _states;
        private readonly EventManager _eventManager;

        public GameStateMachine(IEnumerable<IGameState> states, EventManager eventManager)
        {
            _eventManager = eventManager;
            _states = states.ToDictionary(x => x.GetType());
        }

        public void Initialize()
        {
            ChangeState<DefaultState>();
        }

        public bool ChangeState<T>() where T : IGameState
        {
            if(CurrentState is T)
            {
                return false;
            }

            if(_states.TryGetValue(typeof(T), out IGameState state))
            {
                if(!state.CanEnterFrom(CurrentState))
                {
                    CL.Log($"Can't enter {state.GetType().Name} from {CurrentState?.GetType().Name}", Logs.StateMachineLogs, LogType.Warning);
                    return false;
                }

                if(CurrentState != null)
                {
                    CurrentState.ExitState();
                    _stateStack.Push(CurrentState);
                }

                EnterTheState(state);
                return true;
            }

            CL.Log($"No state with type {typeof(T)} has been found", logType: LogType.Error);
            return false;
        }

        public void QuitStateIfCurrent<T>() where T : IGameState
        {
            if(CurrentState is not T || CurrentState is DefaultState)
            {
                return;
            }

            CurrentState.ExitState();

            if(_stateStack.Count > 0)
            {
                EnterTheState(_stateStack.Pop());
            } else
            {
                ChangeState<DefaultState>();
            }
        }

        public void QuitToDefaultState()
        {
            CurrentState?.ExitState();
            CurrentState = null;
            ChangeState<DefaultState>();
            _stateStack.Clear();
        }

        private void EnterTheState<T>(T state) where T : IGameState
        {
            IGameState previousState = CurrentState;

            CL.Log($"Enter state: from {previousState?.GetType().Name} to {state?.GetType().Name}", Logs.StateMachineLogs);

            CurrentState = state;
            CurrentState?.EnterState();
            _eventManager.Fire(new GameStateChangeEvent(previousState, CurrentState));
        }

        public IGameState CurrentState { get; private set; }
    }
}