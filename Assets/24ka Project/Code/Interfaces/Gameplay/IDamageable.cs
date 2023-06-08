using Code.Gameplay.Systems.LifeDamage;

namespace Code.Interfaces.Gameplay
{
    internal interface IDamageable
    {
        CausedDamage ApplyDamage(CausedDamage damage);
    }
}
