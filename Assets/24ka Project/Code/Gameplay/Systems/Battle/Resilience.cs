using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using System;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle
{
    public class Resilience
    {
        public readonly ElementalResistance Resistance;

        private readonly ElementalAttribute _status;
        private readonly int _statusOverloadValue = 10;

        private readonly float _weakenStatusTime = 3f;
        private float _weakenStatusTimer = 0f;

        public int Health { get; private set; }

        public event Action<ElementalAttributeType> StatusOverloaded;
        public event Action<int> Dead;

        public Resilience(int health, ElementalAttribute status = null, int statusOverloadValue = 10)
        {
            Resistance = new ElementalResistance();
            Health = health;

            _status = status;
            _statusOverloadValue = statusOverloadValue;
        }
        
        public CausedDamage ApplyDamage(CausedDamage damage)
        {
            var lastStatus = _status?.Clone() as ElementalAttribute;
            var causedDamageValue = Health;

            var resistedAttribute = Resistance.Resist(damage.Attribute);            

            if (_status != null)
            {
                _status.Apply(resistedAttribute);

                if (_status.Strength >= _statusOverloadValue)
                {
                    StatusOverloaded?.Invoke(_status.Type);
                    _status.Weaken(1000);
                }
            }

            Health -= damage.Value;

            if(Health <= 0)
            {
                Health = 0;
                Dead?.Invoke(damage.Value);
            }
            else
            {
                causedDamageValue = damage.Value;
            }

            return new CausedDamage
            {
                Value = causedDamageValue,
                Attribute = lastStatus,
                Point = damage.Point,
            };
        }

        public void Update()
        {
            if (_status == null)
                return;

            _weakenStatusTimer += Time.deltaTime;
            if(_weakenStatusTimer > _weakenStatusTime)
            {
                _weakenStatusTimer = 0f;
                _status.Weaken(1);
            }
        }
    }
}
