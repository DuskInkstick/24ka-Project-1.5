using UnityEngine;

namespace Code.Gameplay.Systems.Attack.AttackPatterns
{
    public class AttackPattern
    {
        private Transform _spawnPoint;
        private AttackingObject _attackingObject;

        public AttackPattern(Transform spawnPoint, AttackingObject attackingObject)
        {
            _spawnPoint = spawnPoint;
            _attackingObject = attackingObject;
        }

        public void Attack(Vector2 direction)
        {
            var attacking = GameObject.Instantiate(_attackingObject, _spawnPoint.position, Quaternion.identity);
            attacking.LifeTime = 5f;
            attacking.Direction = direction;
        }
    }
}
