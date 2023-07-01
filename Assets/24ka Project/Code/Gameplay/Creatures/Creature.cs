using Code.Gameplay.Configuration.Animation;
using Code.Gameplay.Configuration.Battle.AttackPerfomance;
using Code.Gameplay.Creatures.AI;
using Code.Gameplay.State;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Movements;
using UnityEngine;

namespace Code.Gameplay.Creatures
{
    public class Creature : CreatureBase
    {
        [Header("Move State")]
        [Tooltip("Optional field")]
        [SerializeField] private AttackBehaviorConfig _moveAttackConfig;
        [SerializeField] private DirectionAnimationConfig _moveAnimation;

        [Header("Attack State")]
        [SerializeField] private AttackBehaviorConfig _attackConfig;
        [SerializeField] private DirectionAnimationConfig _attackAnimation;

        private CreatureAI _ai;

        public virtual void Initialize(Collider2D[] observableCreatures)
        {
            _ai = new AngryAI(GetComponent<Collider2D>(), observableCreatures, new DiscreteDirections(6));
            var movement = new Movement(transform, 1f);

            States.Add(
                new CreatureMoveState(
                    this, 
                    _moveAnimation.Get(Animator),
                    movement,
                    _moveAttackConfig?.Get(transform, AllyGroup)));

            States.Add(
                new CreatureAttackState(
                    this,
                    _attackAnimation.Get(Animator),
                    movement,
                    _attackConfig?.Get(transform, AllyGroup)));
        }

        protected override void Start()
        {
            base.Start();

            var player = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<Collider2D>();

            Initialize(new[] { player });
        }

        protected virtual void Update()
        {
            _ai.Update();
            Resilience.Update();

            CurrentState.LookIn(_ai.LookVector);
            CurrentState.MoveIn(_ai.MoveVector);

            if (_ai.IsAttacking)
               CurrentState.Attack(true);
            else
                CurrentState.Attack(false);

            CurrentState.Update();
        }
    }
}
