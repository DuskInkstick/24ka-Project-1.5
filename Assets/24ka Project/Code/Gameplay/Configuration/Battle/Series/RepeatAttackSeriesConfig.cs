using Code.Gameplay.Systems.Battle.Patterns;
using Code.Gameplay.Systems.Battle.Enums;
using System.Collections.Generic;
using UnityEngine;
using Code.Gameplay.Systems.Battle.Series;

namespace Code.Gameplay.Configuration.Battle.Series
{
    [CreateAssetMenu(
        fileName = "attack_series",
        menuName = "Scriptable Objects/Repeat Attack Series Config",
        order = 1)]
    internal class RepeatAttackSeriesConfig : AttackSeriesConfig
    {
        [SerializeField] private float _timeBetweenPatterns = 0.5f;

        [SerializeField] private SelectionMode _selectionMode = SelectionMode.Loop;

        public override AttackSeries Create(Transform owner, int allyGroup)
        {
            var attackSet = new List<AttackPattern>(AttackPatterns.Count);

            foreach(var pattern in AttackPatterns)
                attackSet.Add(pattern.Create(owner));

            return new RepeatAttackSeries(attackSet)
            {
                TimeBetweenPatterns = _timeBetweenPatterns,
                SelectionMode = _selectionMode,
                AllyGroup = allyGroup
            };
        }
    }
}
