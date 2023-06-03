using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    internal abstract class MoveState : StateBase, IMovable, IWatcher
    {
        private MoveDirection _moveDirection;
        private MoveDirection _viewDirection; // Can be None

        private Movement _movement;
        private FourSideAnimation _animation;

        public Vector2 ViewDirection { get; private set; } // Can't be None (Zero)

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
            {
                _movement.Move(direction);

                if (_viewDirection == MoveDirection.None)
                    ViewDirection = direction;
            }
            else
            {
                OnMoveStoped();
            }

            Animate();
            OnMoved(direction);
        }

        public void Look(Vector2 direction)
        {
            _viewDirection = direction.ToMoveDirection();

            if (_viewDirection != MoveDirection.None)
                ViewDirection = direction;

            Animate();
            OnLooked(direction);
        }

        public override void Start()
        {
            _animation.SetBool("IsMoving", true);
            _animation.Clean();
        }

        protected virtual void OnMoveStoped() { }

        protected virtual void OnMoved(Vector2 direction) { }

        protected virtual void OnLooked(Vector2 direction) { }

        private void Animate()
        {
            if (_viewDirection != MoveDirection.None)
                _animation.Animate(_viewDirection);
            else
                _animation.Animate(_moveDirection);
        }
    }
}
