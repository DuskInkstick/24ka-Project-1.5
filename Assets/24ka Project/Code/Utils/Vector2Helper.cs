using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Utils
{
    internal static class Vector2Helper
    {
        public static MoveDirection ToMoveDirection(this Vector2 vector2)
        {
            var edge = 0.6f;

            if (vector2.y > edge)
                return MoveDirection.Up;

            else if (vector2.y < -edge)
                return MoveDirection.Down;

            else if (vector2.x > edge)
                return MoveDirection.Right;

            else if (vector2.x < -edge)
                return MoveDirection.Left;

            return MoveDirection.None;
        }

        public static Vector2 GetRandom()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
