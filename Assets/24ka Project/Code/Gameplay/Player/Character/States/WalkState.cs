using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.Player.Character.States
{
    internal class WalkState : CharacterActionState
    {
        public WalkState(IStateSwitcher switcher,
                            Animator animator,
                            Transform transform,
                            float speed,
                            AttackBehavior attackBehavior)
              : base(switcher,
                     new FourSideAnimation(animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                     new Movement(transform, speed),
                     attackBehavior)
        { }

        public override void Focus(bool isFocused)
        {
            if (isFocused == true)
                StateSwitcher.SwitchState<FocusState>();
        }

        protected override void OnMoveStoped()
        {
            StateSwitcher.SwitchState<QuiescentState>();
        }

        public override void UseEscapeSkill()
        {
            StateSwitcher.SwitchState<DashState>();
        }
    }
}
