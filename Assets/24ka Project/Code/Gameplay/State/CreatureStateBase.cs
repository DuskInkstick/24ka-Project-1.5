using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    public abstract class CreatureStateBase
    {
        protected readonly IStateSwitcher StateSwitcher;

        private readonly FourSideAnimation _animation;
        private readonly Movement _movement;
        private readonly AttackBehavior _attackBehavior;

        private readonly bool _canMove;
        private readonly bool _canAttack;

        public Resilience Resilience { get; private set; }
        public Vector2 ViewVector { get; set; } = Vector2.down;
        protected Vector2 LookVector { get; set; } = Vector2.zero;
        protected Vector2 MoveVector { get; set; } = Vector2.zero;

        protected CreatureStateBase(IStateSwitcher stateSwitcher,
                                    FourSideAnimation animation,
                                    Resilience resilience,
                                    Movement movement = null,
                                    AttackBehavior attackBehavior = null)
        {
            StateSwitcher = stateSwitcher;
            Resilience = resilience;

            _animation = animation;
            _movement = movement;
            _attackBehavior = attackBehavior;

            _canMove = _movement != null;
            _canAttack = _attackBehavior != null;
        }

        public virtual void Start() 
        {
            CleanAnimateState();

            Resilience.StatusOverloaded += OnStatusOverloaded;
            Resilience.Dead += OnDead;
        }

        public virtual void Update(float deltaTime)
        {
            if(_canMove && MoveVector.ToMoveDirection() != MoveDirection.None)
                _movement.Move(MoveVector, deltaTime);

            if (_canAttack)
                _attackBehavior.Update(deltaTime);

            Resilience.Update(deltaTime);
        }

        public virtual void Stop()
        {
            Resilience.StatusOverloaded -= OnStatusOverloaded;
            Resilience.Dead -= OnDead;
        }

        public virtual void Attack()
        {
            if (_canAttack == false)
                return;

            _attackBehavior.Attack(ViewVector);
        }

        public virtual void LookIn(Vector2 direction)
        {
            LookVector = direction;
            ViewVector = CalcViewVector();
            Animate();
        }

        public virtual void MoveIn(Vector2 direction)
        {
            if (_canMove == false)
                return;

            MoveVector = direction;
            ViewVector = CalcViewVector();
            Animate();

            if (direction.ToMoveDirection() == MoveDirection.None)
                OnMoveStoped();
        }

        public CausedDamage ApplyDamage(CausedDamage damage)
        {
            return Resilience.ApplyDamage(damage);
        }

        protected virtual Vector2 CalcViewVector()
        {
            if (LookVector.ToMoveDirection() != MoveDirection.None)
                return LookVector;

            if (MoveVector.ToMoveDirection() != MoveDirection.None)
                return MoveVector;

            return ViewVector;
        }

        protected virtual void OnMoveStoped() { }

        protected virtual void OnStatusOverloaded(ElementalAttributeType status) { }

        protected virtual void OnDead(int deadDamage) { }

        protected void Animate()
        {
            _animation.Animate(ViewVector.ToMoveDirection());
        }

        protected void CleanAnimateState()
        {
            _animation.CleanState();
        }
    }
}