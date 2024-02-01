using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using System;

namespace Code.Gameplay.Systems.Battle
{
    public class Resilience
    {
        public readonly ElementalResistance Resistance; 
        public int Health { get; private set; }


        public event Action<ElementalAttributeType> StatusOverloaded
        {
            add => Resistance.StatusOverloaded += value;
            remove => Resistance.StatusOverloaded -= value;
        }

        public event Action<int> Dead;

        public Resilience(int health)
        {
            Resistance = new ElementalResistance();
            Health = health;
        }
        
        public int ApplyDamage(Damage damage)
        {
            Resistance.Apply(damage.Attribute);

            var damageValue = Health;
         
            Health -= damage.Value;

            if(Health < 0)
            {
                Health = 0;
                Dead?.Invoke(damage.Value);
            }
            else
            {
                damageValue = damage.Value;
            }

           return damageValue;
        }

        public void Update()
        {
            Resistance.Update();
        }
    }
}
