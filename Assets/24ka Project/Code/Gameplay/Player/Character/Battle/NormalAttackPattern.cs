using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using UnityEngine;

namespace Code.Gameplay.Player.Character.Battle
{
    internal class NormalAttackPattern : AttackPattern
    {
        public NormalAttackPattern(Transform spawnPoint, Bullet tear) : base(spawnPoint)
        {
            AttackingObjects.Add(tear);
        }
    }
}
