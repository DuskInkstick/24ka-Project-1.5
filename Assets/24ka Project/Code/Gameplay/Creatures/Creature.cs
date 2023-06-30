using Code.Gameplay.Creatures.AI;
using Code.Gameplay.Configuration.Battle.AttackPerfomance;
using Code.Gameplay.State;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Configuration.Animation;

namespace Code.Gameplay.Creatures
{
    internal class Creature : MonoBehaviour, IStateSwitcher, IDamageable
    {
        [SerializeField] private int _allyGroup;
        [SerializeField] private AttackBehaviorConfig _attackConfig;
        [SerializeField] private DirectionAnimationConfig _animationConfig;
        private Animator _animator;

        private CreatureAI _ai;
        private Resilience _resilience;
        
        private List<CreatureStateBase> _states;
        private CreatureStateBase _currentState;

        private Vector2 _lastViewVector;

        public int AllyGroup => _allyGroup;

        public void SwitchState<T>() where T : CreatureStateBase
        {
            var newState = _states.Find(state => state is T);

            if (newState == null)
                throw new InvalidOperationException($"Creature does not contain the state: {typeof(T).Name}");

            _lastViewVector = _currentState.ViewVector;
            _currentState.Stop();

            _currentState = newState;
            _currentState.Start();

            _currentState.LookIn(_lastViewVector);
        }

        public CausedDamage ApplyDamage(CausedDamage damage)
        {
            return _currentState.ApplyDamage(damage, _resilience);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<Collider2D>();

            _ai = new AngryAI(GetComponent<Collider2D>(), new[] { player }, new DiscreteDirections(6));
            _resilience = new Resilience(10);

            var anim = _animationConfig.Get(_animator);
            var movement = new Movement(transform, 1f);
            var attack = _attackConfig.Get(transform, AllyGroup);

            _states = new List<CreatureStateBase>()
            {
                new CreatureMoveState(this, anim, movement, attack)
            };
            _currentState = _states[0];
        }

        private void Update()
        {
            _ai.Update();
            _resilience.Update();

            _currentState.LookIn(_ai.LookVector);
            _currentState.MoveIn(_ai.MoveVector);

            if(_ai.IsAttacking)
                _currentState.Attack();

            _currentState.Update();
        }
    }
}
