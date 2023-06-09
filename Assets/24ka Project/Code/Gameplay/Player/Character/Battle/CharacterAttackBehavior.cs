using Code.Gameplay.Player.Character.Battle;
using Code.Gameplay.Systems.Attack;
using Code.Gameplay.Systems.Attack.Bullets;
using UnityEngine;

namespace Code.Gameplay.Player.Character.Buttle
{
    internal class CharacterAttackBehavior : AttackBehavior
    {
        private readonly float _attackInterval = 0.25f;
        private float _attackIntervalTimer = 0f;
        private bool _canAttack = true;

        public CharacterAttackBehavior(Transform ovner, Bullet tearTemplate) : base(ovner)
        {
            AttackPatterns.Add(new NormalAttackPattern(ovner, tearTemplate));
        }

        public override bool Attack(Vector2 direction)
        {
            if(_canAttack)
            {
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
            if(_attackIntervalTimer >= _attackInterval)
            {
                _canAttack = true;
                _attackIntervalTimer = 0f;
            }
        }
    }
}
