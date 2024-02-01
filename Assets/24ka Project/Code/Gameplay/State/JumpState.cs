using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Assets._24ka_Project.Code.Gameplay.State
{
    internal class JumpState : CreatureStateBase
    {
        private readonly Movement _movement;

        public JumpState(
            IStateSwitcher switcher,
            DirectionAnimation animation,
            Movement movement
            ) : base(switcher, animation)
        {
            _movement = movement;
        }

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
    }
}
