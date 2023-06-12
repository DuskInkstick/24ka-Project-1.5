using Code.Gameplay.Player.Buttle;
using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Movements;
using UnityEngine;

namespace Code.Gameplay.Player.Weapons
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private Bullet _tearTemplate;
        [SerializeField] private ParticleSystem _onAttackingEffect;

        private StaffAnimationController _animation;
        private FollowingMovement _movement;

        public Transform Owner { get; set; }
        public int AllyGroup { get; set; }
        public CharacterAttackBehavior AttackBehavior { get; private set; }

        private void Awake()
        {
            AttackBehavior = new CharacterAttackBehavior(transform, new Vector2(0f, 1.3f), _tearTemplate);
        }

        private void Start()
        {
            _animation = GetComponentInChildren<StaffAnimationController>();
            _movement = new FollowingMovement(transform, Owner);
        }

        private void OnEnable()
        {
            AttackBehavior.Attacking += OnAttacking;
        }

        private void Update()
        {
            _movement.Move(Time.deltaTime);
        }

        private void OnDisable()
        {
            AttackBehavior.Attacking -= OnAttacking;
        }

        private void OnAttacking(int attackNote)
        {
            _onAttackingEffect.Play();
            _animation.PlayAttack();
        }
    }
}
