using Code.Gameplay.Systems.Movements;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Creatures.AI
{
    internal class AngryAI : CreatureAI
    {
        private Collider2D _target;

        private const float _changeMoveTime = 1.5f;
        private float _changeMoveTimer = Random.Range(0f, _changeMoveTime);

        private Vector2 VectorToTarget => (Vector2)_target.transform.position - Position;

        public AngryAI(Collider2D self,
                       IEnumerable<Collider2D> observableCreatures,
                       DiscreteDirections directions)
            : base(self, observableCreatures, directions)
        {
        }

        protected override Vector2 CalculateLook()
        {
            if (_target != null)
                return VectorToTarget;

            return MoveVector == Vector2.zero ? LookVector : MoveVector;
        }

        protected override Vector2 CalculateMovement()
        {
            if(_changeMoveTimer < _changeMoveTime)
            {
                _changeMoveTimer += Time.deltaTime;
                return MoveVector;
            }
            _changeMoveTimer = 0f;

            if(_target != null)           
                return GetVectorTo(_target.bounds.center);

            return GetRandomVector();
        }

        protected override bool CalculateAttack()
        {
            return _target != null;
        }

        protected override void ProcessOthersInSight(IList<(float, Collider2D)> others)
        {
            _target = others.Count == 0 ? null : others[0].Item2;
        }
    }
}
