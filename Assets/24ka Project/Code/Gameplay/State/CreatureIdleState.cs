using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    public class CreatureIdleState : CreatureStateBase
    {
        public CreatureIdleState(IStateSwitcher stateSwitcher,
                                 DirectionAnimation animation) 
            : base(stateSwitcher, animation)
        {
        }

        public override void Update() { }

        public override void MoveIn(Vector2 direction)
        {
            if (direction.ToMoveDirection() != MoveDirection.None)
                StateSwitcher.SwitchState<CreatureMoveState>();
        }

        public override void Attack(bool isPerformed, int stateTag = 0)
        {
            if(isPerformed)
                StateSwitcher.SwitchState<CreatureAttackState>(stateTag);
        }
    }
}
