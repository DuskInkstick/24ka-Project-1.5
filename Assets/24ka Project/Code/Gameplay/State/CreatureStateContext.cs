using Code.Gameplay.Configuration.Animation;
using Code.Interfaces.Architecture;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.State
{
    public class CreatureStateContext : MonoBehaviour, IStateSwitcher
    {
        protected Animator Animator;

        protected List<CreatureStateBase> States;
        protected CreatureStateBase CurrentState;

        [SerializeField] private DirectionAnimationConfig _idleAnimation;
        [SerializeField] private DirectionAnimationConfig _deadAnimation;

        public virtual void Initialize()
        {
            States = new List<CreatureStateBase>
            {
                new CreatureIdleState(this, _idleAnimation.Get(Animator)),
                new CreatureDeadState(this, _deadAnimation.Get(Animator)),
            };
            CurrentState = States[0];
        }

        public virtual bool SwitchState<T>(int tag = 0) where T : CreatureStateBase
        {
            CreatureStateBase newState = null;
            if (tag != 0)
                newState = States.Find(state => state is T && state.Tag == tag);

            if (newState == null)
                newState = States.Find(state => state is T);

            if (newState == null)
                return false;

            var lastViewVector = CurrentState.ViewVector;
            CurrentState.Stop();

            CurrentState = newState;
            CurrentState.Start();

            CurrentState.LookIn(lastViewVector);
            return true;
        }

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
        }
    }
}
