using Code.Gameplay.Configuration.Animation;
using Code.Gameplay.Configuration.Battle.Series;
using Code.Gameplay.Creatures.AI;
using Code.Gameplay.State;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Movements;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Creatures
{
    public class Creature : CreatureBase
    {
        protected Animator Animator;

        [Header("Animation Configs")]
        [SerializeField] private DirectionAnimationConfig _idleAnimation;
        [SerializeField] private DirectionAnimationConfig _deathAnimation;
        [SerializeField] private DirectionAnimationConfig _moveAnimation;
        [SerializeField] private DirectionAnimationConfig _attackAnimation;

        [Header("Attack Configs")]
        [SerializeField] private AttackSeriesConfig _attackConfig;

        private CreatureAI _ai;

        public void Initialize(Collider2D[] observableCreatures)
        {
            _ai = new AngryAI(GetComponent<Collider2D>(), observableCreatures, new DiscreteDirections(6));
            InitializeStates();
            AddTransitions();
        }

        protected override void Start()
        {
            base.Start();

            var player = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<Collider2D>();

            Initialize(new[] { player });
        }

        protected override void Update()
        {
            base.Update();
            _ai.Update();
            CurrentState.Update(_ai.LookVector, _ai.MoveVector, _ai.ActionPoint);
        }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private void InitializeStates()
        {
            States = new List<CreatureStateBase>()
            {
                new IdleState(
                    this, 
                    _idleAnimation.Get(Animator)),
                new MoveState(
                    this, 
                    _moveAnimation.Get(Animator),
                    new Movement(transform, 1f)),
                new AttackState(
                    this, 
                    _attackAnimation.Get(Animator), 
                    _attackConfig.Create(transform, AllyGroup)),
                new DeathState(
                    this,
                    _deathAnimation.Get(Animator)),
            };
            CurrentState = States[0];
        }

        private void AddTransitions()
        {
            foreach (var state in States)
            {
                switch (state)
                {
                    case IdleState idle:
                        idle.SetTransition(IdleStateTransitions);
                        break;
                    case MoveState move:
                        move.SetTransition(MoveStateTransitions);
                        break;
                    case AttackState attack:
                        attack.SetTransition(AttackStateTransitions);
                        break;
                }
            }
        }

        private void IdleStateTransitions(bool isComplited)
        {
            if (GeneralRules(isComplited))
                return;

            if (_ai.IsAttacking)
                SwitchState<AttackState>();
            else if (_ai.MoveVector.ToMoveDirection() != MoveDirection.None)
                SwitchState<MoveState>();
        }

        private void MoveStateTransitions(bool isComplited)
        {
            if (GeneralRules(isComplited))
                return;

            if (_ai.IsAttacking)
                SwitchState<AttackState>();
            else if(_ai.MoveVector.ToMoveDirection() == MoveDirection.None)
                SwitchState<IdleState>();
        }

        private void AttackStateTransitions(bool isComplited)
        {
            if(GeneralRules(isComplited))
                return;

            if(_ai.IsAttacking == false)    
                if(_ai.MoveVector.ToMoveDirection() == MoveDirection.None)
                    SwitchState<IdleState>();
                else 
                    SwitchState<MoveState>();
        }

        private bool GeneralRules(bool isComplited)
        {
            if (isComplited == false)
                return false;

            if (IsDead)
            {
                SwitchState<DeathState>();
                return true;
            }
            return false;
        }
    }
}
