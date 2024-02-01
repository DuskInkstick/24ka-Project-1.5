using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.State
{
    public class MoveState : CreatureStateBase
    {
        private readonly Movement _movement;
        public MoveState(
            IStateSwitcher switcher,
            DirectionAnimation animation,
            Movement movement
            ) : base(switcher, animation)
        {
            _movement = movement;
        }

        public override void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint = default)
        {
            base.Update(lookVector, moveVector);

            _movement.Move(MoveVector);
        }
    }   
}
