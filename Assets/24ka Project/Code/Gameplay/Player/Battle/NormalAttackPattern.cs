using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Player.Battle
{
    internal class NormalAttackPattern : AttackPattern
    {
        private readonly Vector3 _spawnOffset;

        public NormalAttackPattern(Transform spawnPoint, Vector2 offset, Bullet tear)
            : base(spawnPoint, new List<AttackingObject>() { tear })
        {
            AttackingObjects.Add(tear);
            _spawnOffset = offset;
        }

        public override void Attack(Vector2 direction)
        {
            var attacking = SubmitAttackingObject(0, 
                SpawnPoint.position + _spawnOffset, 
                Quaternion.identity,
                5f);

            attacking.Speed = 5f;
            attacking.LifeTime = 5f;
            attacking.Direction = direction;
        }
    }
}
