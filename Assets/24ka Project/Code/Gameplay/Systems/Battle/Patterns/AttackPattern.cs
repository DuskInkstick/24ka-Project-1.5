using Code.Gameplay.Systems.Battle.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Patterns
{
    public abstract class AttackPattern
    {
        private float _executionTimer = 0f;

        public Vector2 SpawnOffset { get; set; } = Vector2.zero;
        public int AllyGroup { get; set; }
        public float ExecutionTime { get; set; } = 1f;
        public bool IsComplited { get; private set; } = false;
        public float AttackObjLifeTime { get; set; } = 10f;

        protected readonly Transform Owner;
        protected readonly List<AttackingObject> AttackingObjects;

        protected AttackPattern(Transform owner, List<AttackingObject> attackings)
        {
            Owner = owner;
            AttackingObjects = attackings;
        }

        public virtual void Start()
        {
            IsComplited = false;
            _executionTimer = ExecutionTime;
        }

        public virtual void Update(Vector2 direction, Vector2 position)
        {
            IsComplited = CalcIsComplited();
        }

        protected virtual bool CalcIsComplited()
        {
            _executionTimer -= Time.deltaTime;

            if (_executionTimer > 0f)
                return false;
                       
            return true;          
        }

        protected AttackingObject SubmitAttackingObject(
            int index, 
            Vector2 spawnPoint,
            Quaternion rotation)
        {
            var attacking = GameObject.Instantiate(
                AttackingObjects[index],
                spawnPoint + SpawnOffset,
                rotation);

            attacking.LifeTime = AttackObjLifeTime;
            attacking.AllyGroup = AllyGroup;
            return attacking;
        }
    }
}
