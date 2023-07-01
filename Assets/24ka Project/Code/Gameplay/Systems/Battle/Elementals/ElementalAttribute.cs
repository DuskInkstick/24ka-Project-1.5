﻿using Code.Gameplay.Systems.Battle.Enums;
using System;

namespace Code.Gameplay.Systems.Battle.Elementals
{
    public class ElementalAttribute : ICloneable
    {
        public ElementalAttributeType Type { get; private set; }
        public int Strength { get; private set; }

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

        public void Weaken(int value)
        {
            if (Strength == 0)
                return;

            if(value > 0)
            {
                Strength -= value;

                if(Strength < 0)
                    Strength = 0;
            }
        }

        private void ApplyIce(ElementalAttribute ice)
        {
            switch (Type)
            {
                case ElementalAttributeType.Water:
                    Type = ElementalAttributeType.Ice;
                    Strength += ice.Strength;
                    Strength *= 2;
                    break;
            }
        }

        private void ApplyWater(ElementalAttribute water)
        {
            switch (Type)
            {
                case ElementalAttributeType.Ice:
                    Strength += water.Strength;
                    break;
            }
        }
    }
}
