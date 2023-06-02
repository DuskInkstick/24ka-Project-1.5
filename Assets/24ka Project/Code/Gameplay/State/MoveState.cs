using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    internal abstract class MoveState : StateBase, IMovable, ILooking
    {
        private MoveDirection _moveDirection;
        private MoveDirection _viewDirection;

        private Movement _movement;
        private FourSideAnimation _animation;

        protected MoveState(IStateSwitcher switcher, Movement movement, FourSideAnimation animation) 
            : base(switcher)
        {
            _movement = movement;
            _animation = animation;
        }

        public void Move(Vector2 direction)
        {
            _moveDirection = direction.ToMoveDirection();
            if (_moveDirection != MoveDirection.None)
                _movement.Move(direction);
            else
                OnMoveStoped();

            Animate();
        }

        public void Look(Vector2 direction)
        {
            _viewDirection = direction.ToMoveDirection();
            Animate();
        }

        protected virtual void OnMoveStoped() { }

        public override void Start()
        {
            _animation.SetBool("IsMoving", true);
            _animation.Clean();
        }

        private void Animate()
        {
            if (_viewDirection != MoveDirection.None)
                _animation.Animate(_viewDirection);
            else
                _animation.Animate(_moveDirection);
        }
    }
}
