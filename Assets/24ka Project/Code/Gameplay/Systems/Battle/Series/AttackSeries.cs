using Code.Gameplay.Systems.Battle.Enums;
using Code.Gameplay.Systems.Battle.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Series
{
    public abstract class AttackSeries
    {
        private int _allyGroup;
        protected readonly List<AttackPattern> Patterns;

        public int AllyGroup
        {
            get => _allyGroup;
            set
            {
                _allyGroup = value;
                foreach (var pattern in Patterns)
                    pattern.AllyGroup = _allyGroup;
            }
        }  

        public SelectionMode SelectionMode { get; set; } = SelectionMode.Loop;
        protected int CurrentPatternIndex { get; set; } = -1;

        protected AttackSeries(List<AttackPattern> patterns)
        {
            Patterns = patterns;
        }

        public virtual void Start()
        {
            CurrentPatternIndex = 0;
        }

        public abstract void Update(Vector2 direction, Vector2 position);

        protected void CallStartInPattern()
        {
            if (SelectionMode == SelectionMode.AllAtOnce)
            {
                foreach (var pattern in Patterns)
                    pattern.Start();

                return;
            }
            Patterns[CurrentPatternIndex].Start();
        }

        protected void CallUpdateInPattern(Vector2 direction, Vector2 position)
        {
            if (SelectionMode == SelectionMode.AllAtOnce)
            {
                foreach (var pattern in Patterns)
                    pattern.Update(direction, position);

                return;
            }
            Patterns[CurrentPatternIndex].Update(direction, position);
        }

        protected bool IsPatternComplited()
        {
            if (SelectionMode == SelectionMode.AllAtOnce)
            {
                foreach (var pattern in Patterns)
                {
                    if (pattern.IsComplited == false)
                        return false;
                }
                return true;
            }
            return Patterns[CurrentPatternIndex].IsComplited;
        }
    }
}
