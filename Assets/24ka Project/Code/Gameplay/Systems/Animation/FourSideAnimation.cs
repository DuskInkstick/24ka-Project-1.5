using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Systems.Animation
{
    public class FourSideAnimation : DirectionAnimation
    {
        private readonly string _upAnim;
        private readonly string _downAnim;
        private readonly string _leftAnim;
        private readonly string _rightAnim;

        private MoveDirection _currenetDirection = MoveDirection.None;

        public FourSideAnimation(Animator animator, string upAnim, string downAnim, string leftAnim, string rightAnim)
            : base(animator)
        {
            _upAnim = upAnim;
            _downAnim = downAnim;
            _leftAnim = leftAnim;
            _rightAnim = rightAnim;
        }

        public override void Animate(MoveDirection moveDirection)
        {
            if (_currenetDirection == moveDirection)
                return;
            
            _currenetDirection = moveDirection;

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    Animator.Play(_upAnim);
                    break;
                case MoveDirection.Down:
                    Animator.Play(_downAnim);
                    break;
                case MoveDirection.Left:
                    Animator.Play(_leftAnim);
                    break;
                case MoveDirection.Right:
                    Animator.Play(_rightAnim);
                    break;
            }
        }

        public override void CleanCurrentDirection()
        {
            _currenetDirection = MoveDirection.None;
        }
    }
}
