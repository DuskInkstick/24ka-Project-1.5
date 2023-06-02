using UnityEngine;

namespace Code.Gameplay.Systems
{
    internal class AttackSystem
    {
        public void Attack(Vector2 direction)
        {
            Debug.Log($"Attack {direction}");
        }
    }
}
