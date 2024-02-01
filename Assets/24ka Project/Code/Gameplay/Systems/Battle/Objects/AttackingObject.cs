using Code.Gameplay.Systems.Battle.Actions;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;

namespace Code.Gameplay.Systems.Battle.Objects
{
    public abstract class AttackingObject : MonoBehaviour
    {
        [SerializeField] private int _id;
        [Range(0, 10)]
        [SerializeField] private int _damage;
        [SerializeField] private ElementalAttributeType _attackAttribute;
        [Range(0, 10)]
        [SerializeField] private int _attributeStrenght;
        [SerializeField] private CausingDamageType _causingDamageType;

        private CausingDamage _causingDamage;
        private float _lifeTime = 0.1f;
        private float _lifeTimeTimer = 0f;

        public int Id => _id;
        public int AllyGroup { get; set; } = 1;
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
         
            _causingDamage = _causingDamageType.GetDamagingAction(_damage, attribute);
            _causingDamage.Complited += DestroyIt;
            _lifeTimeTimer = LifeTime;
        }

        protected virtual void Update()
        {
            if(_lifeTimeTimer > 0)            
                _lifeTimeTimer -= Time.deltaTime;
            else
                DestroyIt();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
            {
                if (target.AllyGroup == AllyGroup)
                    return;

                _causingDamage.Damage(target, transform.position);
            }
        }

        private void DestroyIt()
        {
            _causingDamage.Complited -= DestroyIt;
            Destroy(gameObject);
        }
    }
}
