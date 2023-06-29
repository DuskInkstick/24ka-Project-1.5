using Code.Gameplay.Systems.Battle.AttackPerfomance;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.AttackPerfomance
{
    internal abstract class AttackBehaviorConfig : ScriptableObject
    {
        [SerializeField] protected List<AttackPatternConfig> AttackPatterns;

        public abstract AttackBehavior Get(Transform owner, int allyGroup);
    }
}
