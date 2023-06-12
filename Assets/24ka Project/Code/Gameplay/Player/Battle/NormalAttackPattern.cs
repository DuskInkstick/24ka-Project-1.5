using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using UnityEngine;

namespace Code.Gameplay.Player.Battle
{
    internal class NormalAttackPattern : AttackPattern
    {
        private readonly Vector3 _spawnOffset;

        public NormalAttackPattern(Transform spawnPoint, Vector2 offset, Bullet tear, int allyGroup) 
            : base(spawnPoint, allyGroup)
        {
            AttackingObjects.Add(tear);
            _spawnOffset = offset;
        }

        public override void Attack(Vector2 direction)
        {
            var attacking = GameObject.Instantiate(AttackingObjects[0],
                                                   SpawnPoint.position + _spawnOffset,
                                                   Quaternion.identity);
            attacking.LifeTime = 5f;
            attacking.Direction = direction;
        }
    }
}
