using Code.Gameplay.Systems.Battle.Elementals;

namespace Code.Gameplay.Systems.Battle.Actions
{
    internal class ConstantDamagingAction : DamagingAction
    {
        public ConstantDamagingAction(int damageValue = 1, ElementalAttribute attribute = null)
            : base(damageValue, attribute) { }

        protected override void ConsiderCausedDamage(CausedDamage causedDamage) { }
    }
}
