
namespace Code.Gameplay.Systems.LifeDamage.Actions
{
    internal class ConstantDamagingAction : DamagingAction
    {
        public ConstantDamagingAction(int damageValue = 1, ElementalAttribute attribute = null)
            : base(damageValue, attribute) { }

        protected override void ConsiderCausedDamage(CausedDamage causedDamage) { }
    }
}
