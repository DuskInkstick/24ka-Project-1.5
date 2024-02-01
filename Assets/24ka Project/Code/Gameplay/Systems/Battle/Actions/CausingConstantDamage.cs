using Code.Gameplay.Systems.Battle.Elementals;

namespace Code.Gameplay.Systems.Battle.Actions
{
    internal class CausingConstantDamage : CausingDamage
    {
        public CausingConstantDamage(int damageValue = 1, ElementalAttribute attribute = null)
            : base(damageValue, attribute) { }

        protected override void ConsiderCausedDamage(int damage) { }
    }
}
