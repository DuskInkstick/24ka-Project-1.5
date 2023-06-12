using Code.Gameplay.Player.Battle;
using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using UnityEngine;

namespace Code.Gameplay.Player.Buttle
{
    public class CharacterAttackBehavior : AttackBehavior
    {
        public readonly float AttackInterval = 0.25f;

        private float _attackIntervalTimer = 0f;
        private bool _canAttack = true;

        public CharacterAttackBehavior(Transform ovner, Vector2 spawnOffset, Bullet tearTemplate) : base(ovner, 0)
        {
            AttackPatterns.Add(new NormalAttackPattern(ovner, spawnOffset, tearTemplate, 0));
        }

        public override bool Attack(Vector2 direction, int note = 0)
        {

            if (_canAttack)
            {
                OnAttacking(note);

                AttackPatterns[0].Attack(direction);
                _canAttack = false;
                return true;
            }
            return false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_canAttack)
                return;

            _attackIntervalTimer += deltaTime;
            if(_attackIntervalTimer >= AttackInterval)
            {
                _canAttack = true;
                _attackIntervalTimer = 0f;
            }
        }
    }
}
