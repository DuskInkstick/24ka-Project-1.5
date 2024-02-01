using Code.Gameplay.State;

namespace Code.Interfaces.Architecture
{
    public interface IStateSwitcher
    {
        void SwitchState<T>(int phase = 0) where T : CreatureStateBase;
    } 
}
