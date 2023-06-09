using UnityEngine;

namespace Code.Gameplay.Systems
{
    public class Movement
    {
        private Transform _transform;
        private float _speed;

        public Movement(Transform transform, float speed)
        {
            _speed = speed;
            _transform = transform;
        }

        public virtual void Move(Vector2 direction, float deltaTime)
        {
            _transform.Translate(direction * _speed * deltaTime);
        }
    }
}
