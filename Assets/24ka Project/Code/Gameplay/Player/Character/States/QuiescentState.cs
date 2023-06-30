using Code.Gameplay.Player.Abilities.IceWall;
using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
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

        private readonly IcePlacer _icePlacer;
        public QuiescentState(IStateSwitcher switcher,
                           Animator animator,
                           AttackBehavior attackBehavior,
                           IcePlacer icePlacer)
             : base(switcher,
                    new FourSideAnimation(animator, "idle_up", "idle_down", "idle_left", "idle_right"),
                    null,
                    attackBehavior)
        {
            _icePlacer = icePlacer;
        }

        public override void Focus(bool isFocused)
        {
            _isFocused = isFocused;
        }

        public override void MoveIn(Vector2 direction)
        {
            if (direction.ToMoveDirection() != MoveDirection.None)
                StateSwitcher.SwitchState<WalkState>();
        }

        public override void UseEscapeSkill()
        {
            if (_isFocused)
            {
                _icePlacer.TryPlaceWall(ViewVector);
            }
            else
            {
                StateSwitcher.SwitchState<DashState>();
            }
        }
    }
}
