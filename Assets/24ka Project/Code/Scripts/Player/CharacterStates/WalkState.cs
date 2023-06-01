using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Scripts.Player.CharacterStates
{
    internal class WalkState : StateBase, IMovable
    {
        private Movement _movement;
        private MovementAnimation _movementAnimation;

        public WalkState(Animator animator, Movement movement) : base(animator) 
        {
            _movement = movement;
            _movementAnimation = new MovementAnimation(Animator, "walk_up", "walk_down", "walk_left", "walk_right");
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
