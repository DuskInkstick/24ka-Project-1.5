
using UnityEngine;

namespace Scripts.Player.CharacterStates
{
    internal abstract class StateBase
    {
        protected Animator Animator { get; }

        protected StateBase(Animator animator)
        {
            Animator = animator;
        }
        
        protected virtual void Start() { }
        protected virtual void Stop() { }
    }
}



