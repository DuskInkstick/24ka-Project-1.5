using Code.Gameplay.State;

namespace Code.Interfaces.Architecture
{
    public interface IStateSwither
    {
        void SwithState<T>() where T : StateBase;
    } 
}
