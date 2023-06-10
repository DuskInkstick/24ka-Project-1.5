using Code.Gameplay.Player.Abilities.IceWall;
using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.Player.Character.States
{
    internal class FocusState : CharacterActionState
    {
        private readonly IcePlacer _icePlacer;

        public FocusState(IStateSwitcher switcher,
                          Animator animator,
                          Resilience resilience,
                          Transform transform,
                          float speed,
                          AttackBehavior attackBehavior,
                          IcePlacer icePlacer)
            : base(switcher,
                   new FourSideAnimation(animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                   resilience,
                   new Movement(transform, speed),
                   attackBehavior)
        {
            _icePlacer = icePlacer;
        }

        public override void Focus(bool isFocused)
        {
            if (isFocused == false)
                StateSwitcher.SwitchState<WalkState>();
        }

        protected override void OnMoveStoped()
        {
            StateSwitcher.SwitchState<QuiescentState>();
        }

        public override void UseEscapeSkill()
        {
            _icePlacer.TryPlaceWall(ViewVector);
        }
    }
}
