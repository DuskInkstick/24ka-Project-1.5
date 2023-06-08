using System.Collections.Generic;

namespace Code.Gameplay.Systems.LifeDamage
{
    internal class ElementalResistance
    {
        private readonly List<(ElementalAttributeType type, int strenght)> _resistances;
        public IEnumerable<(ElementalAttributeType type, int strenght)> Resistances => _resistances;

        public ElementalResistance()
        {
            _resistances = new List<(ElementalAttributeType, int)>(1);
        }

        public void Add(ElementalAttributeType resistanceType, int strenght)
        {
            _resistances.Add((resistanceType, strenght));
        }

        public ElementalAttribute Resist(ElementalAttribute attribute)
        {
            if (attribute == null)
                return null;

            for (int i = 0; i < _resistances.Count; i++)
            {
                if (_resistances[i].type == attribute.Type)
                {
                    var punched = attribute.Strength - _resistances[i].strenght;
                    return new ElementalAttribute(attribute.Type, punched > 0 ? punched : 0);
                }
            }
            return attribute;
        }
    }
}
