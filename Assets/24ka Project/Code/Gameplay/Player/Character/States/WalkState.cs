using Code.Gameplay.State;
using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using UnityEngine;

namespace Code.Gameplay.Player.Character.States
{
    internal class WalkState : MoveState, IFocusable
    {
        public WalkState(IStateSwitcher switcher, Transform transform, float speed, Animator animator)
            : base(
                  switcher,
                  new Movement(transform, speed), 
                  new FourSideAnimation(animator, "walk_up", "walk_down", "walk_left", "walk_right")) 
        {  }

        public void Focus(bool isFocused)
        {
            if (isFocused)
                StateSwitcher.SwitchState<FocusState>();
        }

        protected override void OnMoveStoped()
        {
            StateSwitcher.SwitchState<IdleState>();
        }
    }
}
