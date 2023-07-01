using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    public class CreatureDeadState : CreatureStateBase
    {
        public CreatureDeadState(IStateSwitcher stateSwitcher,
                                    DirectionAnimation animation)
            : base(stateSwitcher, animation) { }


        public override void Update() { }

        public override void Attack(bool isPerformed, int stateTag = 0) { }

        public override void MoveIn(Vector2 direction) { }

        public override void LookIn(Vector2 direction)
        {
            if(LookVector.ToMoveDirection() == MoveDirection.None)
                base.LookIn(direction);
        }
    }
}
