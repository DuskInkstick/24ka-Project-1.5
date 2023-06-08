using UnityEngine;

namespace Code.Gameplay.Systems.Attack.Bullets
{
    public class Bullet : AttackingObject
    {
        [SerializeField] private int _speed;

        private Movement _movement;

        protected override void Start()
        {
            base.Start();
            _movement = new Movement(transform, _speed);
        }

        protected override void Update()
        {
            base.Update();
            _movement.Move(Direction, Time.deltaTime);
        }
    }
}
