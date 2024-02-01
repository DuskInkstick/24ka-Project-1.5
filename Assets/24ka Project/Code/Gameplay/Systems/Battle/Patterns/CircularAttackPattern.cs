using Code.Gameplay.Systems.Battle.Objects;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Patterns
{
    internal class CircularAttackPattern : AttackPattern
    {
        private int _currentIndex = 0;
        private float _betweenAttacksTimer = 0f;

        public float TimeBetweenAttacks = 0.5f;
        public float AttackAngel { get; set; } = 360f;
        public float Rotation { get; set; } = 0f;
        public bool ConsiderTargetPosition { get; set; } = false;
        public float InnerRadius { get; set; } = 0f;

        public SelectionMode AttackObjSelectionMode { get; set; } = SelectionMode.Loop;
        public bool ChangeAttackObjInSameAttack { get; set; } = false;

        public int AttackObjCount { get; set; } = 4;
        public bool RotateAttackObj { get; set; } = false;
        public float AttackObjInitSpeed { get; set; } = 1f; 
        public float AttackObjInitRotation { get; set; } = 0f;

        public CircularAttackPattern(Transform spawnPoint, List<AttackingObject> attackings)
            : base(spawnPoint, attackings)
        { }

        public override void Start()
        {
            base.Start();
            _currentIndex = 0;
            _betweenAttacksTimer = 0;
        }

        public override void Update(Vector2 direction, Vector2 targetPosition)
        {
            base.Update(direction, targetPosition);

            if(_betweenAttacksTimer > 0)
            {
                _betweenAttacksTimer -= Time.deltaTime;
                return;
            }

            PerformAttack(direction, targetPosition);
            _betweenAttacksTimer = TimeBetweenAttacks;
        }

        private void PerformAttack(Vector2 direction, Vector2 position)
        {
            var rotationAngel = Rotation;

            if (ConsiderTargetPosition)
            {
                if (direction == Vector2.zero)
                    direction = position - (Vector2)Owner.position;

                rotationAngel += Vector2.SignedAngle(new Vector2(1f, 0f), direction);
            }

            var step = AttackAngel / (AttackObjCount - 1);
            var maxAngel = rotationAngel + AttackAngel;

            for (; rotationAngel <= maxAngel; rotationAngel += step)
            {
                var radians = Mathf.Deg2Rad * rotationAngel;
                var attackDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                var objectRotation = Quaternion.identity;

                if (RotateAttackObj)
                    objectRotation = Quaternion.Euler(0, 0, rotationAngel + AttackObjInitRotation);

                var attacking = SubmitAttackingObject(_currentIndex,
                    (Vector2)Owner.position + attackDirection * InnerRadius,
                    objectRotation);

                if (attacking is Projectile pt)
                {
                    pt.TargetPosition = position;
                    pt.Direction = attackDirection;
                    pt.Speed = AttackObjInitSpeed;
                }

                if (ChangeAttackObjInSameAttack)
                    _currentIndex = AttackObjSelectionMode.Next(_currentIndex, AttackingObjects.Count);
            }

            if (ChangeAttackObjInSameAttack == false)
                _currentIndex = AttackObjSelectionMode.Next(_currentIndex, AttackingObjects.Count);
        }
    }
}
