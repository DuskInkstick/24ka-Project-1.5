using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;


namespace Code.Gameplay.Creatures
{
    internal class CreatureMoveState : CreatureStateBase
    {
        public CreatureMoveState(IStateSwitcher stateSwitcher,
                                 DirectionAnimation animation,
                                 Movement movement = null,
                                 AttackBehavior attackBehavior = null)
            : base(stateSwitcher, animation, movement, attackBehavior)
        {
        }
    }
}
