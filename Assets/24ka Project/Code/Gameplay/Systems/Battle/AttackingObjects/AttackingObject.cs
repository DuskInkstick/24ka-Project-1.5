using Code.Gameplay.Systems.Battle.Actions;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.AttackingObjects
{
    public abstract class AttackingObject : MonoBehaviour
    {
        [SerializeField] private int _id;
        [Range(0, 10)]
        [SerializeField] private int _damage;
        [SerializeField] private ElementalAttributeType _attackAttribute;
        [Range(0, 10)]
        [SerializeField] private int _attributeStrenght;
        [SerializeField] private AttackingActionType _attackingActionType;

        private DamagingAction _damagingAction;
        private float _lifeTime = 0.1f;
        private float _lifeTimeTimer = 0f;

        public int Id => _id;
        public int AllyGroup { get; set; } = 1;
        public Vector2 Direction { get; set; } = Vector2.up;
        public float Speed { get; set; } = 1f;
        public float LifeTime
        {
            get => _lifeTime;
            set
            {
                if(value > 0.1f)
                    _lifeTime = value;
            }
        }

        protected virtual void Start()
        {
            var attribute = new ElementalAttribute(_attackAttribute, _attributeStrenght);
         
            _damagingAction = _attackingActionType.GetDamagingAction(_damage, attribute);
            _damagingAction.Complited += DestroyIt;
        }

        protected virtual void Update()
        {
            _lifeTimeTimer += Time.deltaTime;

            if(_lifeTimeTimer >= LifeTime)
                DestroyIt();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                if (damageable.AllyGroup == AllyGroup)
                    return;

                _damagingAction.Damage(damageable, transform.position);
            }
        }

        private void DestroyIt()
        {
            _damagingAction.Complited -= DestroyIt;
            Destroy(gameObject);
        }
    }
}
