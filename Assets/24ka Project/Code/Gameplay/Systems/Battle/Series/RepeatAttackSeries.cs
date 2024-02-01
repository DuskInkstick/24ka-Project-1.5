using Code.Gameplay.Systems.Battle.Enums;
using Code.Gameplay.Systems.Battle.Patterns;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Series
{
    public class RepeatAttackSeries : AttackSeries
    {
        private bool _attackStarted = false;
        private float _betweenPatternsTimer = 0f;

        public float TimeBetweenPatterns { get; set; } = 0f;       

        public RepeatAttackSeries(List<AttackPattern> patterns)
            : base(patterns)
        { }

        public override void Start()
        {
            base.Start();
            _betweenPatternsTimer = 0f;
            _attackStarted = false;
        }

        public override void Update(Vector2 direction, Vector2 targetPosition)
        {
            if(_betweenPatternsTimer > 0f)
            {
                _betweenPatternsTimer -= Time.deltaTime;
                return;
            }

            if(_attackStarted == false)
            {
                CurrentPatternIndex = SelectionMode.Next(CurrentPatternIndex, Patterns.Count);
                CallStartInPattern();
                _attackStarted = true;
            }

            CallUpdateInPattern(direction, targetPosition);

            if (IsPatternComplited())
            {
                _attackStarted = false;
                _betweenPatternsTimer = TimeBetweenPatterns;
            }          
        }
    }
}
