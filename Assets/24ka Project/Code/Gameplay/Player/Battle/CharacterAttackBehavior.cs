using Code.Gameplay.Player.Battle;
using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Player.Buttle
{
    public class CharacterAttackBehavior : RepeatAttackBehavior
    {
        public CharacterAttackBehavior(Transform ovner,
                                       Vector2 spawnOffset,
                                       Bullet tearTemplate)
            : base(ovner, new List<AttackPattern>())
        {
            AttackSet.Add(new NormalAttackPattern(ovner, spawnOffset, tearTemplate));
            AttackInterval = 0.25f;
        }
    }
}
