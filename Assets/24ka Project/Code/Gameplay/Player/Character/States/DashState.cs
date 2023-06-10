using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Battle;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Player.Character.States
{
    internal class DashState : CharacterActionState
    {
        private float _dashTime = 0.25f;
        private float _dashTimer = 0f;

        public DashState(IStateSwitcher stateSwitcher,
                         Animator animator,
                         Resilience resilience,
                         Transform transform,
                         float speed)
            : base(stateSwitcher,
                   new FourSideAnimation(animator, "dash_up", "dash_down", "dash_left", "dash_right"),
                   resilience,
                   new Movement(transform, speed))
        { }

        public override void Start()
        {
            _dashTimer = 0f;
            ViewVector = Vector2.zero;
            MoveVector = Vector2.zero;

            base.Start();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _dashTimer += deltaTime;
            if (_dashTimer > _dashTime)
                StateSwitcher.SwitchState<WalkState>();
        }

        public override void MoveIn(Vector2 direction)
        {
            if (MoveVector.ToMoveDirection() != MoveDirection.None)
                return;

            base.MoveIn(direction);
        }

        public override void LookIn(Vector2 direction)
        {
            if (ViewVector != Vector2.zero)
                return;

            base.LookIn(direction);
        }

        public override void Attack() { }
    }
}
