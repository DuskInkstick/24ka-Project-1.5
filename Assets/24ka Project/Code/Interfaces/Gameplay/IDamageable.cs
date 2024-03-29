﻿using Code.Gameplay.Systems.Battle;

namespace Code.Interfaces.Gameplay
{
    internal interface IDamageable
    {
        int AllyGroup { get; }
        int ApplyDamage(Damage damage);
    }
}
