using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.State
{
    internal class CreatureDeadState : CreatureStateBase
    {
        protected CreatureDeadState(IStateSwitcher stateSwitcher,
                                 DirectionAnimation animation)
            : base(stateSwitcher, animation) { }


        public override void Update() { }

        public override void Attack() { }

        public override void MoveIn(Vector2 direction) { }

        protected override Vector2 CalcViewVector() => ViewVector;
    }
}
