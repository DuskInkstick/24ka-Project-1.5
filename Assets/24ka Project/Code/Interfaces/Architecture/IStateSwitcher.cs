using Code.Gameplay.State;

namespace Code.Interfaces.Architecture
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : StateBase;
    } 
}
