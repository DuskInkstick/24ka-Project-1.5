using Code.Gameplay.State;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Gameplay;
using UnityEngine;

namespace Code.Gameplay.Creatures
{
    public abstract class CreatureBase : CreatureStateContext, IDamageable
    {     
        [SerializeField] private int _allyGroup = 1;
        [SerializeField] private int _health = 10;
        protected Resilience Resilience;

        public int AllyGroup => _allyGroup;
        protected bool IsDead { get; private set; }

        public virtual int ApplyDamage(Damage damage)
        {
            return Resilience.ApplyDamage(damage);
        }

        protected virtual void Start()
        {
            Resilience = new Resilience(_health);

            Resilience.Dead += OnDead;
        }

        protected override void Update()
        {
            base.Update();
            Resilience.Update();
        }

        protected virtual void OnDestroy()
        {
            Resilience.Dead -= OnDead;
        }

        private void OnDead(int deadDamage) => IsDead = true;  
    }
}
