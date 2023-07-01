using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;

namespace Code.Gameplay.State
{
    public class CreatureMoveState : CreatureStateBase
    {
        public CreatureMoveState(IStateSwitcher stateSwitcher,
                                 DirectionAnimation animation,
                                 Movement movement = null,
                                 AttackBehavior attackBehavior = null) 
            : base(stateSwitcher, animation, movement, attackBehavior)
        {
        }

        public override void Attack(bool isPerformed, int stateTag = 0)
        {
            if (isPerformed)
                StateSwitcher.SwitchState<CreatureAttackState>(stateTag);
            else
                base.Attack(true);

        }

        protected override void OnMoveStoped()
        {
            StateSwitcher.SwitchState<CreatureIdleState>();
        }
    }   
}
