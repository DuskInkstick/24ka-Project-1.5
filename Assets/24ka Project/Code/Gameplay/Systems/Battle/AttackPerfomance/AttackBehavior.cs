using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackPerfomance
{
    public abstract class AttackBehavior
    {
        private int _allyGroup;

        protected readonly List<AttackPattern> AttackPatterns;
        protected readonly Transform Ovner;

        public event Action<int> Attacking;
        public event Action<int> Attacked;

        protected AttackBehavior(Transform ovner, int allyGroup)
        {
            Ovner = ovner;
            AttackPatterns = new List<AttackPattern>();
            _allyGroup = allyGroup;
        }

        public abstract bool Attack(Vector2 direction, int note = 0);

        public virtual void Update(float deltaTime)
        {
            foreach (var pattern in AttackPatterns)
                pattern.Update(deltaTime);
        }

        protected void OnAttacking(int attackNote)
        {
            Attacking?.Invoke(attackNote);
        }

        protected void OnAttacked(int attackNote)
        {
            Attacked?.Invoke(attackNote);
        }
    }
}
