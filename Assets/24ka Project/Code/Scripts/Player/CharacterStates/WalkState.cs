using Scripts.Interfaces;
using UnityEngine;

namespace Player.CharacterStates
{
    internal class WalkState : StateBase, IMovable
    {
        private Movement _movement;
        public WalkState(Movement movement)
        {
            _movement = movement;
        }
        public void Move(Vector2 direction)
        {
            _movement.Move(direction);
        }
    }
}
