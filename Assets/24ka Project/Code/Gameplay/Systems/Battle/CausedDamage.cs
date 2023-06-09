using Code.Gameplay.Systems.Battle.Elementals;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle
{
    public class CausedDamage
    {
        public ElementalAttribute Attribute { get; set; }
        public Vector2 Point { get; set; }

        private int _value = 0;
        public int Value
        {
            get => _value; 
            set 
            {
                if(value >= 0)
                    _value = value;
            }
        }
    }
}
