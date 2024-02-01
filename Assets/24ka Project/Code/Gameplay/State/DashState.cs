using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._24ka_Project.Code.Gameplay.State
{
    internal class DashState : CreatureStateBase
    {
        private readonly Movement _movement;

        public DashState(
            IStateSwitcher switcher, 
            DirectionAnimation animation
            ) : base(switcher, animation)
        { }

        public override void Start()
        {
            base.Start();

            if (ExecutionTime <= 0f)
                ExecutionTime = 0.25f;
        }

        public override void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint = default)
        {
            base.Update(lookVector, moveVector);

            _movement.Move(MoveVector);
        }


        public override void SetMoveVector(Vector2 direction)
        {
            if (MoveVector.ToMoveDirection() == MoveDirection.None)
                base.SetMoveVector(direction);
        }

        public override void SetLookVector(Vector2 direction)
        {
            if (LookVector.ToMoveDirection() == MoveDirection.None)
                base.SetLookVector(direction);
        }
    }
}
