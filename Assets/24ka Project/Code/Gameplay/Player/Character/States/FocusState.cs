using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Attack;
using Code.Gameplay.Systems.LifeDamage;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.Player.Character.States
{
    internal class FocusState : CharacterActionState
    {
        public FocusState(IStateSwitcher switcher,
                          Animator animator,
                          Resilience resilience,
                          Transform transform,
                          float speed,
                          AttackBehavior attackBehavior)
            : base(switcher,
                   new FourSideAnimation(animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                   resilience,
                   new Movement(transform, speed),
                   attackBehavior)
        { }

        public override void Focus(bool isFocused)
        {
            if (isFocused == false)
                StateSwitcher.SwitchState<WalkState>();
        }

        protected override void OnMoveStoped()
        {
            StateSwitcher.SwitchState<QuiescentState>();
        }
    }
}
