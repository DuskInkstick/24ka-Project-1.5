﻿using UnityEngine;

namespace Player
{
    internal class Movement
    {
        private Transform _transform;
        private float _speed;

        public Movement(Transform transform, float speed)
        {
            _speed = speed;
            _transform = transform;
        }

        public virtual void Move(Vector2 direction)
        {
            _transform.Translate(Vector3.Normalize(direction) * _speed);
        }
    }
}
