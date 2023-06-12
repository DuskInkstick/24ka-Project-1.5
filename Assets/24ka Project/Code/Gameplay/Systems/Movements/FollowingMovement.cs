using UnityEngine;

namespace Code.Gameplay.Systems.Movements
{
    internal class FollowingMovement
    {
        private readonly Transform _self;
        private readonly Transform _target;
        private readonly float _minDistance;
        private readonly float _speed;

        private readonly float _maxDistance = 10f;

        public FollowingMovement(Transform self, Transform target, float minDistance = 0.4f, float speed = 3.5f)
        {
            _self = self;
            _target = target;
            _minDistance = minDistance;
            _speed = speed;
        }

        public void Move(float deltaTime)
        {
            var targetDistance = Vector3.Distance(_target.position, _self.position);

            if (targetDistance >= _maxDistance)
            {
                _self.position = _target.position;
                return;
            }

            var moveDirection = _target.position - _self.position;

            if (targetDistance <= _minDistance - 0.1f)
            {
                _self.Translate(-moveDirection * _speed * Time.deltaTime);
            }
            else if (targetDistance >= _minDistance + 0.1f)
            {
                _self.Translate(moveDirection * _speed * targetDistance * deltaTime);
            }
        }
    }
}
