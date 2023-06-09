﻿using Code.Gameplay.Systems.Battle.Actions;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Gameplay;
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
        private float _lifeTimeTimer = 0f;

        public int Id => _id;
        public int AllyGroup { get; set; }
        public Vector2 Direction { get; set; }
        public float LifeTime { get; set; }

        protected virtual void Start()
        {
            var attribute = new ElementalAttribute(_attackAttribute, _attributeStrenght);
            switch (_attackingActionType)
            {
                case AttackingActionType.Normal:
                    _damagingAction = new DamagingAction(_damage, attribute);
                    break;
                case AttackingActionType.Penetrated:
                    _damagingAction = new PenetratedDamagingAction(_damage, attribute);
                    break;
                case AttackingActionType.Constant:
                    _damagingAction = new ConstantDamagingAction(_damage, attribute);
                    break;
            }

            _damagingAction.Complited += DestroyIt;

            if (LifeTime < 0.2f)
                LifeTime = 0.2f;
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
