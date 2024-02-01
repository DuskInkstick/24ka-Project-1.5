using Code.Gameplay.Systems.Battle.Enums;
using System;

namespace Code.Gameplay.Systems.Battle.Elementals
{
    public class ElementalAttribute : ICloneable
    {
        public ElementalAttributeType Type { get; set; }
        private int _strenght = 0;
        public int Strength
        {
            get => _strenght;
            set
            {
                if(value >= 0)
                    _strenght = value;
                else
                    _strenght = 0;
            }
        }

        public ElementalAttribute(
            ElementalAttributeType type = ElementalAttributeType.None,
            int strength = 0)
        {
            Type = type;
            Strength = strength;
        }

        public object Clone()
        {
            return new ElementalAttribute(Type, Strength);
        }

        public void Apply(ElementalAttribute attribute)
        {
            if (attribute == null || attribute.Type == ElementalAttributeType.None)
                return;

            if (Type == attribute.Type)
            {
                Strength += attribute.Strength;
                return;
            }

            if(Type == ElementalAttributeType.None)
            {
                Type = attribute.Type;
                Strength = attribute.Strength;
                return;
            }

            switch (attribute.Type)
            {
                case ElementalAttributeType.Ice:
                    ApplyIce(attribute);
                    break;
                case ElementalAttributeType.Water:
                    ApplyWater(attribute);
                    break;
            }
        }

        private void ApplyIce(ElementalAttribute ice)
        {
            switch (Type)
            {
                case ElementalAttributeType.Water:
                    Type = ElementalAttributeType.Ice;
                    Strength += ice.Strength * 2;
                    break;
            }
        }

        private void ApplyWater(ElementalAttribute water)
        {
            switch (Type)
            {
                case ElementalAttributeType.Ice:
                    Strength += water.Strength * 2;
                    break;
            }
        }
    }
}
