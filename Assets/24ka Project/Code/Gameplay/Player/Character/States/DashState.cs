using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Movements;
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
                         Transform transform,
                         float speed)
            : base(stateSwitcher,
                   new FourSideAnimation(animator, "dash_up", "dash_down", "dash_left", "dash_right"),
                   new Movement(transform, speed))
        { }

        public override void Start()
        {
            base.Start();

            _dashTimer = 0f;
        }

        public override void Update()
        {
            base.Update();

            _dashTimer += Time.deltaTime;
            if (_dashTimer > _dashTime)
                StateSwitcher.SwitchState<WalkState>();
        }

        public override void MoveIn(Vector2 direction)
        {
            if (MoveVector.ToMoveDirection() == MoveDirection.None)
                base.MoveIn(direction);
        }

        public override void LookIn(Vector2 direction)
        {
            if (LookVector.ToMoveDirection() == MoveDirection.None)
                base.LookIn(direction);
        }

        public override void Attack() { }
    }
}
