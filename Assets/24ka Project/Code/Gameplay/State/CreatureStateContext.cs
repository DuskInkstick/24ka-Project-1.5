using Code.Interfaces.Architecture;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.State
{
    public abstract class CreatureStateContext : MonoBehaviour, IStateSwitcher
    {
        protected List<CreatureStateBase> States;
        protected CreatureStateBase CurrentState;

        public virtual void SwitchState<T>(int phase = 0) where T : CreatureStateBase
        {
            CreatureStateBase newState = null;
            if (phase != 0)
                newState = States.Find(state => state is T && state.Phase == phase);

            if (newState == null)            
                newState = States.Find(state => state is T);             
            
            if (newState == null)
                throw new ArgumentException($"No such state: {typeof(T)}");

            CurrentState.Stop();
            var lastViewVector = CurrentState.ViewVector;

            CurrentState = newState;
            CurrentState.Start();
            CurrentState.ViewVector = lastViewVector;
        }

        protected virtual void Update()
        {
            CurrentState.TrySwithStateByTransition();
        }
    }
}
