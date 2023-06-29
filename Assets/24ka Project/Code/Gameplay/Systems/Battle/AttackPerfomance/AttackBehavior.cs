using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackPerfomance
{
    public abstract class AttackBehavior
    {
        private int _allyGroup;
        public int AllyGroup
        {
            get => _allyGroup;
            set
            {
                _allyGroup = value;
                foreach (var attack in AttackSet)
                    attack.AllyGroup = _allyGroup;
            }
        }

        protected readonly List<AttackPattern> AttackSet;
        protected readonly Transform Ovner;

        public event Action<int> Attacking;
        public event Action<int> Attacked;

        protected AttackBehavior(Transform ovner, List<AttackPattern> attacks)
        {
            Ovner = ovner;
            AttackSet = attacks;
        }

        public abstract bool Attack(Vector2 direction, int note = 0);

        public virtual void Update() 
        {
            foreach (var pattern in AttackSet)
                pattern.Update();
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
