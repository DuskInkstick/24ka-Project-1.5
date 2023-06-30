using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    public abstract class CreatureStateBase
    {
        protected readonly IStateSwitcher StateSwitcher;

        private readonly DirectionAnimation _animation;
        private readonly Movement _movement;
        private readonly AttackBehavior _attackBehavior;

        private readonly bool _canMove;
        private readonly bool _canAttack;

        public Vector2 ViewVector { get; private set; } = Vector2.down;
        protected Vector2 LookVector { get; set; } = Vector2.zero;
        protected Vector2 MoveVector { get; set; } = Vector2.zero;

        protected CreatureStateBase(IStateSwitcher stateSwitcher,
                                    DirectionAnimation animation,
                                    Movement movement = null,
                                    AttackBehavior attackBehavior = null)
        {
            StateSwitcher = stateSwitcher;

            _animation = animation;
            _movement = movement;
            _attackBehavior = attackBehavior;

            _canMove = _movement != null;
            _canAttack = _attackBehavior != null;
        }

        public virtual void Start() 
        {
            CleanAnimateDirection();
            LookVector = Vector2.zero;
            MoveVector = Vector2.zero;
        }

        public virtual void Update()
        {
            if (_canMove && MoveVector.ToMoveDirection() != MoveDirection.None)
                _movement.Move(MoveVector);

            if (_canAttack)
                _attackBehavior.Update();
        }

        public virtual void Attack()
        {
            if (_canAttack)
                _attackBehavior.Attack(ViewVector);
        }

        public virtual CausedDamage ApplyDamage(CausedDamage damage, Resilience resilience)
        {
            return resilience.ApplyDamage(damage);
        }

        public virtual void LookIn(Vector2 direction)
        {
            if (LookVector.Equals(direction))
                return;

            LookVector = direction;
            ViewVector = CalcViewVector();
            Animate();
        }

        public virtual void MoveIn(Vector2 direction)
        {
            if (_canMove == false)
                return;

            if (direction.ToMoveDirection() == MoveDirection.None)
                OnMoveStoped();

            if (MoveVector.Equals(direction))
                return;

            MoveVector = direction;
            ViewVector = CalcViewVector();
            Animate();
        }

        public virtual void OnStatusOverloaded(ElementalAttributeType status) { }

        public virtual void OnDead(int deadDamage) { }

        public virtual void Stop() { }

        protected virtual Vector2 CalcViewVector()
        {
            if (LookVector.ToMoveDirection() != MoveDirection.None)
                return LookVector;

            if (MoveVector.ToMoveDirection() != MoveDirection.None)
                return MoveVector;

            return ViewVector;
        }

        protected virtual void OnMoveStoped() { }

        protected void Animate()
        {
            _animation.Animate(ViewVector.ToMoveDirection());
        }

        protected void CleanAnimateDirection()
        {
            _animation.CleanCurrentDirection();
        }
    }
}