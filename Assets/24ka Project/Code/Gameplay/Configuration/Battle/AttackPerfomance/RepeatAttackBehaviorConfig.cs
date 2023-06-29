using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Battle.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.AttackPerfomance
{
    [CreateAssetMenu(
        fileName = "attack_behavior",
        menuName = "Scriptable Objects/Repeat Attack Behavior Config",
        order = 1)]
    internal class RepeatAttackBehaviorConfig : AttackBehaviorConfig
    {
        [SerializeField] private float _attackInterval = 1f;
        [SerializeField] private int _attacksPerSet = 1;
        [SerializeField] private float _addIntervalBetweenSets = 0f;

        [SerializeField] private LoopMode _loopMode = LoopMode.Loop;

        public override AttackBehavior Get(Transform owner, int allyGroup)
        {
            var attackSet = new List<AttackPattern>(AttackPatterns.Count);

            foreach(var attackSetup in AttackPatterns)
            {
                attackSet.Add(attackSetup.Get(owner));
            }
            var behavior = new RepeatAttackBehavior(owner, attackSet);

            behavior.AttackInterval = _attackInterval;
            behavior.AttacksPerSet = _attacksPerSet;
            behavior.AddIntervalBetweenSets = _addIntervalBetweenSets;
            behavior.LoopMode = _loopMode;

            behavior.AllyGroup = allyGroup;
            return behavior;
        }
    }
}
