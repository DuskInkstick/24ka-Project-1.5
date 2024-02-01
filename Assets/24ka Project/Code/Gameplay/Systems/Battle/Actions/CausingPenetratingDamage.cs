using Code.Gameplay.Systems.Battle.Elementals;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Actions
{
    internal class CausingPenetratingDamage : CausingDamage
    {
        public CausingPenetratingDamage(int damageValue = 1, ElementalAttribute attribute = null) 
            : base(damageValue, attribute) { }

        protected override Damage CompileDamage(Vector2 point)
        {
            var damage = base.CompileDamage(point);
            damage.Value = 1;
            return damage;
        }
    }
}
