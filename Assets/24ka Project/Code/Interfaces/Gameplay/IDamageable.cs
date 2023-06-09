using Code.Gameplay.Systems.Battle;

namespace Code.Interfaces.Gameplay
{
    internal interface IDamageable
    {
        CausedDamage ApplyDamage(CausedDamage damage);
    }
}
