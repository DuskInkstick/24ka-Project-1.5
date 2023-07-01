using Code.Gameplay.State;

namespace Code.Interfaces.Architecture
{
    public interface IStateSwitcher
    {
        bool SwitchState<T>(int tag = 0) where T : CreatureStateBase;
    } 
}
