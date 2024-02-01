using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;

namespace Code.Gameplay.State
{
    public class IdleState : CreatureStateBase
    {
        public IdleState(
            IStateSwitcher switcher,
            DirectionAnimation animation
            ) : base(switcher, animation)
        { }
    }
}
