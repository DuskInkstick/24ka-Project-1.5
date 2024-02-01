using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle.Series;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using System;
using UnityEngine;

namespace Code.Gameplay.State
{
    public class AttackState : CreatureStateBase
    {
        private readonly bool _canMove;
        private readonly Movement _movement;
        private readonly AttackSeries _attackSeries;

        public AttackState(
            IStateSwitcher switcher,
            DirectionAnimation animation,
            AttackSeries attackSeries,
            Movement movement = null
            ) : base(switcher, animation)
        {
            _canMove = movement != null;
            _movement = movement;
            _attackSeries = attackSeries;

            if (attackSeries == null)
                throw new ArgumentException("No AttackSeries");
        }

        public override void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint = default)
        {
            base.Update(lookVector, moveVector);

            if (_canMove)
                _movement.Move(MoveVector);

            _attackSeries.Update(LookVector, actionPoint);
        }
    }
}