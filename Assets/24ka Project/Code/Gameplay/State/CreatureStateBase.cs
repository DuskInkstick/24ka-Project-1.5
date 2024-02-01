using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;
using Code.Utils;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    public abstract class CreatureStateBase
    {
        protected readonly IStateSwitcher StateSwitcher;
        private readonly DirectionAnimation _animation;
        private float _executionTimer = 0f;

        public int Phase { get; set; } = 0;
        public float ExecutionTime { get; set; } = 0f;
        public bool IsComplited { get; protected set; } = true;

        public Vector2 ViewVector { get; set; } = Vector2.down;
        protected Vector2 LookVector { get; set; } = Vector2.zero;
        protected Vector2 MoveVector { get; set; } = Vector2.zero;

        private Action<bool> _trySwithStateByTransition;

        protected CreatureStateBase(IStateSwitcher switcher, DirectionAnimation animation)
        {
            _animation = animation;
            StateSwitcher = switcher;
        }

        public void SetTransition(Action<bool> rule)
        {
            _trySwithStateByTransition = null;
            _trySwithStateByTransition = rule;
        }

        public void TrySwithStateByTransition()
        {
            if(_trySwithStateByTransition != null)
                _trySwithStateByTransition(IsComplited);
        }

        public virtual void Start() 
        {
            _animation.Start();

            LookVector = Vector2.zero;
            MoveVector = Vector2.zero;

            if (ExecutionTime > 0f)
            {
                IsComplited = false;
                _executionTimer = ExecutionTime;
            }
        }

        public virtual void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint = default)
        {
            SetLookVector(lookVector);
            SetMoveVector(moveVector);

            ViewVector = CalcViewVector();
            Animate();

            if(IsComplited == false)
            {
                _executionTimer -= Time.deltaTime;

                if (_executionTimer <= 0f)
                    IsComplited = true;
            }
        }

        public virtual void Stop() { }

        public virtual void SetLookVector(Vector2 direction)
        {
            LookVector = direction;
        }

        public virtual void SetMoveVector(Vector2 direction)
        {
            MoveVector = direction;
        }

        protected virtual Vector2 CalcViewVector()
        {
            if (LookVector.ToMoveDirection() != MoveDirection.None)
                return LookVector;

            if (MoveVector.ToMoveDirection() != MoveDirection.None)
                return MoveVector;

            return ViewVector;
        }
            
        protected void Animate()
        {
            _animation.Animate(ViewVector.ToMoveDirection());
        }
    }
}