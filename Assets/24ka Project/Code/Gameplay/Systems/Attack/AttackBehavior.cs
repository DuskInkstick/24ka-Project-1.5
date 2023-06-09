using Code.Gameplay.Systems.Attack.AttackPatterns;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Attack
{
    public abstract class AttackBehavior
    {
        protected readonly List<AttackPattern> AttackPatterns;
        protected readonly Transform Ovner;

        protected AttackBehavior(Transform ovner)
        {
            Ovner = ovner;
            AttackPatterns = new List<AttackPattern>();
        }

        public abstract bool Attack(Vector2 direction);

        public virtual void Update(float deltaTime)
        {
            foreach (var pattern in AttackPatterns)
                pattern.Update(deltaTime);
        }
    }
}
