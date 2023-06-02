using Code.Gameplay.State;
using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Player.Character.States
{
    internal class IdleState : StateBase, IMovable, ILooking
    {
        private readonly FourSideAnimation _animation;
        private MoveDirection _viewDirection;

        public IdleState(IStateSwitcher stateSwitcher, Animator animator) : base(stateSwitcher)
        {
            _animation = new FourSideAnimation(animator, "idle_up", "idle_down", "idle_left", "idle_right");
        }
        public void Move(Vector2 direction)
        {
            if(direction.ToMoveDirection() != MoveDirection.None)
                StateSwitcher.SwitchState<WalkState>();
        }

        public void Look(Vector2 direction)
        {
            _viewDirection = direction.ToMoveDirection();
            _animation.Animate(_viewDirection);
        }

        public override void Start()
        {
            _animation.Clean();
            _animation.SetBool("IsMoving", false);
        }
    }
}
