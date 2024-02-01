using Code.Gameplay.Configuration.Battle.Patterns;
using Code.Gameplay.Systems.Battle.Series;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.Series
{
    internal abstract class AttackSeriesConfig : ScriptableObject
    {
        [SerializeField] protected List<AttackPatternConfig> AttackPatterns;

        public abstract AttackSeries Create(Transform owner, int allyGroup);
    }
}
