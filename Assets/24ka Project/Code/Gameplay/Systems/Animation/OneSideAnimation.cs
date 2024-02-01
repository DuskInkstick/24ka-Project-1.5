using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Systems.Animation
{
    public class OneSideAnimation : DirectionAnimation
    {
        private readonly string _anim;
        private MoveDirection _currentDirection;

        public OneSideAnimation(Animator animator, string anim) : base(animator)
        {
            _anim = anim;
        }

        public override void Start()
        {
            _currentDirection = MoveDirection.None;
        }

        public override void Animate(MoveDirection direction)
        {
            if (_currentDirection != MoveDirection.None)
                return;

            _currentDirection = direction;
            Animator.Play(_anim);
        }
    }
}
