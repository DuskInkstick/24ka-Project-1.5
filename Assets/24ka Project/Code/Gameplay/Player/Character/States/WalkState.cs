using Code.Gameplay.State;
using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using UnityEngine;

namespace Code.Gameplay.Player.Character.States
{
    internal class WalkState : MoveStateBase, IFocusable
    {
        public WalkState(IStateSwither swither, Transform transform, float speed, Animator animator)
            : base(
                  swither,
                  new Movement(transform, speed), 
                  new MovementAnimation(animator, "walk_up", "walk_down", "walk_left", "walk_right")) 
        {  }

        public void Focus(bool isFocused)
        {
            if (isFocused)
                StateSwither.SwithState<FocusState>();
        }
    }
}
