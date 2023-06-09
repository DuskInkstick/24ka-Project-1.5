using Code.Gameplay.Systems;
using Code.Gameplay.Systems.LifeDamage;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Code.Gameplay.State
{
    internal abstract class CreatureDeadState : CreatureStateBase
    {
        public CreatureDeadState(IStateSwitcher stateSwitcher,
                                 FourSideAnimation animation,
                                 Resilience resilience)
            : base(stateSwitcher, animation, resilience) { }

        public override void Start()
        {
            CleanAnimateState();
            Resilience.StatusOverloaded += OnStatusOverloaded;
        }

        public override void Update(float deltaTime)
        {
            Resilience.Update(deltaTime);
        }

        public override void Stop()
        {
            Resilience.StatusOverloaded -= OnStatusOverloaded;
        }

        public override void Attack() { }

        public override void MoveIn(Vector2 direction) { }

        protected override Vector2 CalcViewVector() => ViewVector;
    }
}
