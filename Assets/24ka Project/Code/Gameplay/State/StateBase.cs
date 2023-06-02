
using Code.Interfaces.Architecture;

namespace Code.Gameplay.State
{
    public abstract class StateBase
    {
        protected IStateSwither StateSwither { get; }

        protected StateBase(IStateSwither stateSwither)
        {
            StateSwither = stateSwither;
        }

        public virtual void Start() { }
        public virtual void Stop() { }
    }
}