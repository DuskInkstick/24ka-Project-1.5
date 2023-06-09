using Code.Gameplay.Systems.Attack.AttackPatterns;
using Code.Gameplay.Systems.Attack.Bullets;
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
