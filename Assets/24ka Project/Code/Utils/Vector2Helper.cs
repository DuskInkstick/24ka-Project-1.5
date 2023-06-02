using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Utils
{
    internal static class Vector2Helper
    {
        public static MoveDirection ToMoveDirection(this Vector2 vector2)
        {
            var edge = 0.6f;

            MoveDirection moveDirection;

            if (vector2.y > edge)
                moveDirection = MoveDirection.Up;

            else if (vector2.y < -edge)
                moveDirection = MoveDirection.Down;

            else if (vector2.x > edge)
                moveDirection = MoveDirection.Right;

            else if (vector2.x < -edge)
                moveDirection = MoveDirection.Left;

            else
                moveDirection = MoveDirection.None;

            return moveDirection;
        }
    }
}
