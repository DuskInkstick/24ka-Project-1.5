using Code.Gameplay.Systems.Battle.Elementals;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Actions
{
    internal class PenetratedDamagingAction : DamagingAction
    {
        public PenetratedDamagingAction(int damageValue = 1, ElementalAttribute attribute = null) 
            : base(damageValue, attribute) { }

        protected override CausedDamage CompileDamage(Vector2 point)
        {
            var damage = base.CompileDamage(point);
            damage.Value = 1;
            return damage;
        }
    }
}
