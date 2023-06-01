using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Scripts.Player.CharacterStates
{
    public class MovementAnimation
    {
        private Animator _animator;

        private string _upAnim;
        private string _downAnim;
        private string _leftAnim;
        private string _rightAnim;

        private MoveDirection _currenetMovementDirection = MoveDirection.None;

        public MovementAnimation(Animator animator, string upAnim, string downAnim, string leftAnim, string rightAnim)
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
            
            if(moveDirection == MoveDirection.None)
            {
                _animator.SetBool("IsMoving", false);
                return;
            }

            _animator.SetBool("IsMoving", true);

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
    }
}
