using Code.Interfaces.Gameplay;
using System;
using UnityEngine;

namespace Code.Gameplay.Systems.LifeDamage.Actions
{
    internal class DamagingAction
    {
        private int _damageValue;
        private ElementalAttribute _attribute;

        public event Action Complited;
        
        public DamagingAction(int damageValue = 1, ElementalAttribute attribute = null)
        {
            _damageValue = damageValue;
            _attribute = attribute;
        }

        public void Damage(IDamageable target, Vector2 point)
        {
            if (_damageValue <= 0)
                return;

            var damage = CompileDamage(point);
            var causedDamage = target.ApplyDamage(damage);
            ConsiderCausedDamage(causedDamage);

            if (_damageValue <= 0)
                Complited?.Invoke();
        } 

        protected virtual CausedDamage CompileDamage(Vector2 point)
        {
            return new CausedDamage
            {
                Value = _damageValue,
                Attribute = _attribute,
                Point = point
            };
        }

        protected virtual void ConsiderCausedDamage(CausedDamage causedDamage)
        {
            _damageValue -= causedDamage.Value;

            if (_damageValue > 0)
            {
               _attribute.Apply(causedDamage.Attribute);
            }
        }
    }
}
