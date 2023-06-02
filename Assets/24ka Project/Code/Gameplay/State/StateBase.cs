
using Code.Interfaces.Architecture;

namespace Code.Gameplay.State
{
    public abstract class StateBase
    {
        protected IStateSwitcher StateSwitcher { get; }

        protected StateBase(IStateSwitcher stateSwitcher)
        {
            StateSwitcher = stateSwitcher;
        }

        public virtual void Start() { }
        public virtual void Stop() { }
    }
}