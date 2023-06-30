using Code.Gameplay.State;
using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;


namespace Code.Gameplay.Creatures
{
    internal class CreatureMoveState : CreatureStateBase
    {
        public CreatureMoveState(IStateSwitcher stateSwitcher,
                                 FourSideAnimation animation,
                                 Movement movement = null,
                                 AttackBehavior attackBehavior = null)
            : base(stateSwitcher, animation, movement, attackBehavior)
        {
        }
    }
}
