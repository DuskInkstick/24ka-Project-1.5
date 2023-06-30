using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Systems.Animation
{
    public abstract class DirectionAnimation
    {
        protected readonly Animator Animator;

        public DirectionAnimation(Animator animator)
        {
            Animator = animator;
        }

        public abstract void Animate(MoveDirection direction);

        public abstract void CleanCurrentDirection();

        public void SetBool(string paramName, bool value)
        {
            Animator.SetBool(paramName, value);
        }
    }
}
