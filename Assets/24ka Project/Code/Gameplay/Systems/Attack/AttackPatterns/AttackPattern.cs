using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Gameplay.Systems.Attack.AttackPatterns
{
    public abstract class AttackPattern
    {
        protected Transform SpawnPoint;
        protected List<AttackingObject> AttackingObjects;

        public AttackPattern(Transform spawnPoint)
        {
            SpawnPoint = spawnPoint;
            AttackingObjects = new List<AttackingObject>();
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
