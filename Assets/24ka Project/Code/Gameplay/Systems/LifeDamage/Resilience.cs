using System;

namespace Code.Gameplay.Systems.LifeDamage
{
    public class Resilience
    {
        public readonly ElementalResistance Resistance;

        private readonly ElementalAttribute _activeStatus;
        private readonly int _statusOverloadLimit = 10;

        private readonly float _weakenStatusTime = 3f;
        private float _weakenStatusTimer = 0f;

        public int Health { get; private set; }

        public event Action<ElementalAttributeType> StatusOverloaded;
        public event Action<int> Dead;

        public Resilience(int health, ElementalAttribute activeStatus = null, int statusOverloadLimit = 10)
        {
            Resistance = new ElementalResistance();
            Health = health;

            _activeStatus = activeStatus;
            _statusOverloadLimit = statusOverloadLimit;
        }
        
        public CausedDamage ApplyDamage(CausedDamage damage)
        {
            var lastaActiveStatus = _activeStatus?.Clone() as ElementalAttribute;
            var causedDamageValue = Health;

            var resistedAttribute = Resistance.Resist(damage.Attribute);            

            if (_activeStatus != null)
            {
                _activeStatus.Apply(resistedAttribute);

                if (_activeStatus.Strength >= _statusOverloadLimit)
                {
                    StatusOverloaded?.Invoke(_activeStatus.Type);
                    _activeStatus.Weaken(1000);
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
                Attribute = lastaActiveStatus,
                Point = damage.Point,
            };
        }

        public void Update(float deltaTime)
        {
            _weakenStatusTimer += deltaTime;
            if(_weakenStatusTimer > _weakenStatusTime)
            {
                _weakenStatusTimer = 0f;
                _activeStatus.Weaken(1);
            }
        }
    }
}
