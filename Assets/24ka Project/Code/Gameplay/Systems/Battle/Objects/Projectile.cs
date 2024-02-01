using Code.Gameplay.Systems.Movements;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Objects
{
    public class Projectile : AttackingObject
    {
        private Movement _movement;

        public Vector2 TargetPosition { get; set; }
        public Vector2 Direction { get; set; } = Vector2.up;
        public float Speed { get; set; } = 1f;

        protected override void Start()
        {
            base.Start();
            _movement = new Movement(transform, Speed);
        }

        protected override void Update()
        {
            base.Update();
            _movement.Move(Direction);
        }
    }
}
