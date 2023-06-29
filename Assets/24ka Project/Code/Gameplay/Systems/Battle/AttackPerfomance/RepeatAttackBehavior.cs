using Code.Gameplay.Systems.Battle.Enums;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackPerfomance
{
    public class RepeatAttackBehavior : AttackBehavior
    {
        private int _currentAttackIndex = 0;
        private bool _canAttack = true;

        public float AttackInterval { get; set; }
        private float _attackIntervalTimer = 0f;

        public int AttacksPerSet { get; set; } = 1;
        private int _attacksMade = 0;

        public float AddIntervalBetweenSets { get; set; } = 0f;
        public LoopMode LoopMode { get; set; } = LoopMode.Loop;

        public RepeatAttackBehavior(Transform ovner, List<AttackPattern> attacks)
            : base(ovner, attacks)
        {
        }

        public override bool Attack(Vector2 direction, int note = 0)
        {
            if (_canAttack)
            {
                OnAttacking(note);

                AttackSet[_currentAttackIndex].Attack(direction);
                _canAttack = false;
                _attacksMade++;

                return true;
            }
            return false;
        }

        public override void Update()
        {
            base.Update();

            if (_canAttack)
                return;

            _attackIntervalTimer += Time.deltaTime;

            if (_attacksMade < AttacksPerSet && _attackIntervalTimer >= AttackInterval)
            {
                _canAttack = true;
                _attackIntervalTimer = 0f;
            }
            else if (_attackIntervalTimer >= AttackInterval + AddIntervalBetweenSets)
            {
                _currentAttackIndex = LoopMode.Next(_currentAttackIndex, AttackSet.Count);
                _attacksMade = 0;

                _canAttack = true;
                _attackIntervalTimer = 0f;
            }
        }
    }
}
