using Assets._24ka_Project.Code.Gameplay.Systems.Battle.Patterns;
using Code.Gameplay.Systems.Battle.Objects;
using Code.Gameplay.Systems.Battle.Patterns;
using Code.Gameplay.Systems.Battle.Series;
using Code.Gameplay.Systems.Movements;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Player.Weapons
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private Projectile _tearTemplate;
        [SerializeField] private ParticleSystem _onAttackingEffect;

        private StaffAnimationController _animation;
        private FollowingMovement _movement;

        public Transform Owner { get; set; }
        public int AllyGroup { get; set; }
        public RepeatAttackSeries AttackSeries { get; private set; }

        public void PlayAttack()
        {
            _onAttackingEffect.Play();
            _animation.PlayAttack();
        }

        private void Awake()
        {
            AttackSeries = new RepeatAttackSeries(
                new List<AttackPattern>
                {
                    new OneAttackPattern(transform, _tearTemplate)
                    {
                        AttackObjInitSpeed = 12f,
                        AllyGroup = AllyGroup,
                        AttackObjLifeTime = 5f,
                    }
                })
            {
                AllyGroup = AllyGroup,
                TimeBetweenPatterns = 0.05f,
            };
        }

        private void Start()
        {
            _animation = GetComponentInChildren<StaffAnimationController>();
            _movement = new FollowingMovement(transform, Owner);
        }

        private void Update()
        {
            _movement.Move(Time.deltaTime);
        }    
    }
}
