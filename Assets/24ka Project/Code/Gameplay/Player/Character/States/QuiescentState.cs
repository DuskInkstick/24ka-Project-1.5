using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Attack;
using Code.Gameplay.Systems.LifeDamage;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Player.Character.States
{
    internal class QuiescentState : CharacterActionState
    {
        // Subject to change 
        private bool _isFocused = false;

        public QuiescentState(IStateSwitcher switcher,
                           Animator animator,
                           Resilience resilience,
                           AttackBehavior attackBehavior)
             : base(switcher,
                    new FourSideAnimation(animator, "idle_up", "idle_down", "idle_left", "idle_right"),
                    resilience,
                    null,
                    attackBehavior)
        { }

        public override void Focus(bool isFocused)
        {
            _isFocused = isFocused;
        }

        public override void MoveIn(Vector2 direction)
        {
            if(direction.ToMoveDirection() != MoveDirection.None)
                StateSwitcher.SwitchState<WalkState>();
        }
    }
}
