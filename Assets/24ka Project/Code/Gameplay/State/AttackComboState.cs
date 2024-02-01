using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle.Series;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using System;
using UnityEngine;

namespace Assets._24ka_Project.Code.Gameplay.State
{
    internal class AttackComboState : AttackState
    {
        public int NextPhase { get; set; } = 0;
        public float ToNextPhaseTime { get; set; } = 0f;
        private float _toNextPhaseTimer = 0f;

        public AttackComboState(
            IStateSwitcher switcher,
            DirectionAnimation animation,
            AttackSeries attackSeries,
            Movement movement = null
            ) : base(switcher, animation, attackSeries, movement)
        { }

        public override void Start()
        {
            base.Start();
            _toNextPhaseTimer = ToNextPhaseTime;

            if (ToNextPhaseTime <= 0f || ToNextPhaseTime > ExecutionTime)
                throw new ArgumentOutOfRangeException("ToNextPhaseTime must be > 0 and <= ExecutionTime");
        }

        public override void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint = default)
        {
            base.Update(lookVector, moveVector, actionPoint);

            if (_toNextPhaseTimer > 0)
                _toNextPhaseTimer -= Time.deltaTime;
            else
                StateSwitcher.SwitchState<AttackComboState>(NextPhase);
        }
    }
}
