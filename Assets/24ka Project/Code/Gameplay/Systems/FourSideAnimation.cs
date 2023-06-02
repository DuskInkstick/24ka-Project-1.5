using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Systems
{
    public class FourSideAnimation
    {
        private Animator _animator;

        private string _upAnim;
        private string _downAnim;
        private string _leftAnim;
        private string _rightAnim;

        private MoveDirection _currenetMovementDirection = MoveDirection.None;

        public FourSideAnimation(Animator animator, string upAnim, string downAnim, string leftAnim, string rightAnim)
        {
            _animator = animator;
            _upAnim = upAnim;
            _downAnim = downAnim;
            _leftAnim = leftAnim;
            _rightAnim = rightAnim;
        }

        public void Animate(MoveDirection moveDirection)
        {
            if (_currenetMovementDirection == moveDirection)
                return;
            
            _currenetMovementDirection = moveDirection;

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    _animator.Play(_upAnim);
                    break;
                case MoveDirection.Down:
                    _animator.Play(_downAnim);
                    break;
                case MoveDirection.Left:
                    _animator.Play(_leftAnim);
                    break;
                case MoveDirection.Right:
                    _animator.Play(_rightAnim);
                    break;
            }
        }

        public void SetBool(string paramName, bool value)
        {
            _animator.SetBool(paramName, value);
        }

        public void Clean()
        {
            _currenetMovementDirection = MoveDirection.None;
        }
    }
}
