using Code.Gameplay.Systems.Battle.AttackingObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackPerfomance
{
    public abstract class AttackPattern
    {
        private int _allyGroup;

        protected Transform SpawnPoint;
        protected List<AttackingObject> AttackingObjects;

        public AttackPattern(Transform spawnPoint, int allyGroup)
        {
            SpawnPoint = spawnPoint;
            AttackingObjects = new List<AttackingObject>();
            _allyGroup = allyGroup;
        }

        public virtual void Attack(Vector2 direction)
        {
            var attacking = GameObject.Instantiate(AttackingObjects[0], SpawnPoint.position, Quaternion.identity);
            attacking.LifeTime = 5f;
            attacking.Direction = direction;
        }

        public virtual void Update(float deltTime) { }
    }
}
