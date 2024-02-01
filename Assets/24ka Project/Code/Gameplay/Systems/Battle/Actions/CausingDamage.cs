using Code.Gameplay.Systems.Battle.Elementals;
using Code.Interfaces.Gameplay;
using System;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Actions
{
    internal class CausingDamage
    {
        private int _totalDamage;
        private ElementalAttribute _attribute;

        public event Action Complited;
        
        public CausingDamage(int totalDamage = 1, ElementalAttribute attribute = null)
        {
            _totalDamage = totalDamage;
            _attribute = attribute;
        }

        public void Damage(IDamageable target, Vector2 place)
        {
            if (_totalDamage <= 0)
                return;

            var damage = CompileDamage(place);
            var causedDamage = target.ApplyDamage(damage);

            ConsiderCausedDamage(causedDamage);

            if (_totalDamage <= 0)
                Complited?.Invoke();
        } 

        protected virtual Damage CompileDamage(Vector2 place)
        {
            return new Damage
            {
                Value = _totalDamage,
                Attribute = _attribute,
                Place = place
            };
        }

        protected virtual void ConsiderCausedDamage(int damage)
        {
            _totalDamage -= damage;
        }
    }
}
