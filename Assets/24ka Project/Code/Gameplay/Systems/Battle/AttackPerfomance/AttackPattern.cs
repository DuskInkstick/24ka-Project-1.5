using Code.Gameplay.Systems.Battle.AttackingObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackPerfomance
{
    public abstract class AttackPattern
    {
        public int AllyGroup { get; set; }
        public Vector2 SpawnOffset { get; set; } = Vector2.zero;

        protected readonly Transform SpawnPoint;
        protected readonly List<AttackingObject> AttackingObjects;

        protected AttackPattern(Transform spawnPoint, List<AttackingObject> attackings)
        {
            SpawnPoint = spawnPoint;
            AttackingObjects = attackings;
        }

        public abstract void Attack(Vector2 direction);

        public virtual void Update() { }

        protected AttackingObject SubmitAttackingObject(
            int index, 
            Vector2 spawnPoint,
            Quaternion rotation, 
            float lifeTime)
        {
            var attacking = GameObject.Instantiate(AttackingObjects[index],
                                                   spawnPoint + SpawnOffset,
                                                   rotation);
            attacking.LifeTime = lifeTime;
            attacking.AllyGroup = AllyGroup;
            return attacking;
        }
    }
}
