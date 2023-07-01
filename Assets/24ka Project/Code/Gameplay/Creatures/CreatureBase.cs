using Code.Gameplay.State;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using UnityEngine;

namespace Code.Gameplay.Creatures
{
    public class CreatureBase : CreatureStateContext, IDamageable
    {
        protected Resilience Resilience;

        [SerializeField] private int _allyGroup = 1;
        [SerializeField] private int _health = 10;

        public int AllyGroup => _allyGroup;

        public virtual CausedDamage ApplyDamage(CausedDamage damage)
        {
            return CurrentState.ApplyDamage(damage, Resilience);
        }

        protected virtual void Start()
        {
            Initialize();
            Resilience = new Resilience(_health, new ElementalAttribute());

            Resilience.StatusOverloaded += OnStatusOverloaded;
            Resilience.Dead += OnDead;
        }

        protected virtual void OnDestroy()
        {
            Resilience.StatusOverloaded -= OnStatusOverloaded;
            Resilience.Dead -= OnDead;
        }

        protected virtual void OnDead(int deadDamage)
        {
            SwitchState<CreatureDeadState>();
        }

        protected virtual void OnStatusOverloaded(ElementalAttributeType type) { }
    }
}
