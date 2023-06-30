using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Systems.Animation
{
    public class TwoSideAnimation : DirectionAnimation
    {
        private readonly string _positiveAnim;
        private readonly string _negativeAnim;

        private MoveDirection _currentDirX;
        private MoveDirection _currentDirY;
        public TwoSideAnimation(Animator animator, string positiveAnim, string negativeAnim)
            : base(animator)
        {
            _positiveAnim = positiveAnim;
            _negativeAnim = negativeAnim;
        }

        public override void Animate(MoveDirection direction)
        {
            if (_currentDirY == direction || _currentDirX == direction)
                return;

            if (direction == MoveDirection.Up || direction == MoveDirection.Right)
            {
                Animator.Play(_positiveAnim);
                _currentDirX = MoveDirection.Right;
                _currentDirY = MoveDirection.Up;
            }
            else if(direction == MoveDirection.Down || direction == MoveDirection.Left)
            {
                Animator.Play(_negativeAnim);
                _currentDirX = MoveDirection.Left;
                _currentDirY = MoveDirection.Down;
            }
        }

        public override void CleanCurrentDirection()
        {
            _currentDirX = MoveDirection.None;
            _currentDirY = MoveDirection.None;
        }
    }
}
