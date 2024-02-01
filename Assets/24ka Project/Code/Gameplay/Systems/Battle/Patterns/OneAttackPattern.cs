using Code.Gameplay.Systems.Battle.Objects;
using Code.Gameplay.Systems.Battle.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._24ka_Project.Code.Gameplay.Systems.Battle.Patterns
{
    internal class OneAttackPattern : AttackPattern
    {
        public float AttackObjInitSpeed { get; set; } = 1f;
        public float AttackObjInitRotation { get; set; } = 0f;
        public bool RotateAttackObj { get; set; } = false;

        public OneAttackPattern(
            Transform owner,
            AttackingObject attackObj
            ) : base(owner, new List<AttackingObject> { attackObj })
        { }


        public override void Update(Vector2 direction, Vector2 targetPosition)
        {
            base.Update(direction, targetPosition);

            var rotationAngel = 0f;

            if (RotateAttackObj)
                rotationAngel = Vector2.SignedAngle(new Vector2(1f, 0f), direction);

            var obj = SubmitAttackingObject(
                0, 
                Owner.position, 
                Quaternion.Euler(0, 0, rotationAngel + AttackObjInitRotation));

            if(obj is Projectile pt)
            {
                pt.Speed = AttackObjInitSpeed;
                pt.Direction = direction;
                pt.TargetPosition = targetPosition;
            }
        }

        protected override bool CalcIsComplited()
        {
            return true;
        }
    }
}
