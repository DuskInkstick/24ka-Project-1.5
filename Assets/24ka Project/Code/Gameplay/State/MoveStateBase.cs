using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    internal abstract class MoveStateBase : StateBase, IMovable
    {
        private Movement _movement;
        private MovementAnimation _movementAnimation;
        protected MoveStateBase(IStateSwither swither, Movement movement, MovementAnimation animation) 
            : base(swither)
        {
            _movement = movement;
            _movementAnimation = animation;
        }

        public void Move(Vector2 direction)
        {
            var moveDirection = direction.ToMoveDirection();
            if (moveDirection != MoveDirection.None)
                _movement.Move(direction);

            _movementAnimation.Animate(moveDirection);
        }
    }
}
