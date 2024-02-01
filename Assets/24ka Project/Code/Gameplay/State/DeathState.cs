using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.State
{
    public class DeathState : CreatureStateBase
    {
        private bool _updated = false;
        public DeathState(
            IStateSwitcher switcher,
            DirectionAnimation animation
            ): base(switcher, animation)
        { }

        public override void Start()
        {
            base.Start();
            _updated = false;
        }

        public override void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint = default)
        {
            if( _updated == false)
            {
                base.Update(lookVector, moveVector);
                _updated = true;
            }
        }
    }
}
