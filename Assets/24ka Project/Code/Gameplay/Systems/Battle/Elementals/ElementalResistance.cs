using Code.Gameplay.Systems.Battle.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Elementals
{
    public class ElementalResistance
    {
        private readonly ElementalAttribute _currentStatus;
        private readonly int _statusOverloadValue = 100;

        private readonly float _weakenStatusTime = 3f;
        private float _weakenStatusTimer = 0f;

        private readonly List<(ElementalAttributeType type, int strenght)> _resistances;
        public IEnumerable<(ElementalAttributeType type, int strenght)> Resistances => _resistances;

        public event Action<ElementalAttributeType> StatusOverloaded;

        public ElementalResistance()
        {
            _resistances = new List<(ElementalAttributeType, int)>(1);
            _currentStatus = new ElementalAttribute();
        }

        public void Add(ElementalAttributeType resistanceType, int strenght)
        {
            _resistances.Add((resistanceType, strenght));
        }

        public ElementalAttribute Apply(ElementalAttribute attribute)
        {
            if (attribute == null)
                return null;

            for (int i = 0; i < _resistances.Count; i++)
            {
                if (_resistances[i].type == attribute.Type)
                {
                    var punched = attribute.Strength - _resistances[i].strenght;
                    attribute.Strength = punched > 0 ? punched : 0;
                }
            }

            _currentStatus.Apply(attribute);

            if (_currentStatus.Strength >= _statusOverloadValue)
            {
                StatusOverloaded?.Invoke(_currentStatus.Type);
                _currentStatus.Strength = 0;
            }

            return attribute;
        }

        public void Update()
        {
            if (_currentStatus == null)
                return;

            _weakenStatusTimer += Time.deltaTime;
            if (_weakenStatusTimer > _weakenStatusTime)
            {
                _weakenStatusTimer = 0f;
                _currentStatus.Strength -= 10;
            }
        }
    }
}
