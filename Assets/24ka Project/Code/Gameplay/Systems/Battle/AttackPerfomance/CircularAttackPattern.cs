using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackPerfomance
{
    internal class CircularAttackPattern : AttackPattern
    {
        private int _currentIndex = 0;

        public int Count { get; set; } = 4;
        public float AttackAngel { get; set; } = 360f;
        public float Rotation { get; set; } = 0f;
        public bool FreezeRotaion { get; set; } = false;
        public float Radius { get; set; } = 5f;
        public float InnerRadius { get; set; } = 0f;
        public LoopMode LoopMode { get; set; } = LoopMode.Loop;
        public bool DifferentInSameAttack { get; set; } = false;

        public bool RotateAttackingObjects { get; set; } = false;
        public float InitSpeed { get; set; } = 1f; 

        private float LifeTime => (Radius - InnerRadius) / InitSpeed;

        public CircularAttackPattern(Transform spawnPoint, List<AttackingObject> attackings)
            : base(spawnPoint, attackings)
        {
        }

        public override void Attack(Vector2 direction)
        {
            var rotationAngel = Rotation;

            if(FreezeRotaion == false)
                rotationAngel += Vector2.SignedAngle(new Vector2(1f, 0f), direction);

            var step = AttackAngel / Count;
            var maxAngel = rotationAngel + AttackAngel;

            for (; rotationAngel < maxAngel; rotationAngel += step)
            {
                var radians = Mathf.Deg2Rad * rotationAngel;
                var attackDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                var objectRotation = Quaternion.identity;

                if (RotateAttackingObjects)
                    objectRotation = Quaternion.Euler(0, 0, rotationAngel);

                var attacking = SubmitAttackingObject(_currentIndex,
                    (Vector2)SpawnPoint.position + attackDirection * InnerRadius,
                    objectRotation,
                    LifeTime);

                attacking.Direction = attackDirection;
                attacking.Speed = InitSpeed;

                if (DifferentInSameAttack)
                    _currentIndex = LoopMode.Next(_currentIndex, AttackingObjects.Count);
            }

            if(DifferentInSameAttack == false)
                _currentIndex = LoopMode.Next(_currentIndex, AttackingObjects.Count);
        }
    }
}
