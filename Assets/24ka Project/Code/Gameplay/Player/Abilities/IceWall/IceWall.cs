using Code.Gameplay.Systems.Battle;
using Code.Interfaces.Gameplay;
using Code.Utils;
using System.Collections;
using UnityEngine;

namespace Code.Gameplay.Player.Abilities.IceWall
{
    public class IceWall : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _allyGroup = 0;
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private float _placeIceDelay = 0.5f;

        private Animator _animator;
        private IceWallPart[] _ice;
        private Resilience _resilience;

        private int[] _breakingOrder;
        private int _breakeIndex = -1;

        public int AllyGroup => _allyGroup;

        public CausedDamage ApplyDamage(CausedDamage damage)
        {
            Instantiate(_hitEffect, damage.Point, Quaternion.identity);

            BreakIce(damage.Value);

            return _resilience.ApplyDamage(damage);
        }

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _ice = gameObject.GetComponentsInChildren<IceWallPart>();

            _resilience = new Resilience(6);

            _breakingOrder = new int[_ice.Length];

            GenerateIce();
            StartCoroutine(ShowIce());
        }

        private void BreakIce(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _breakeIndex++;

                if (_breakeIndex < _breakingOrder.Length)
                {
                    _ice[_breakingOrder[_breakeIndex]].IsBroken = true;
                    _animator.Play("shaking");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        private void GenerateIce()
        {
            for (int i = 0; i < _ice.Length; i++)
            {
                if (_ice[i].IceSize == 0)
                    _ice[i].SpriteIndex = Random.Range(0, 2);
                else
                    _ice[i].SpriteIndex = Random.Range(2, 5);

                _breakingOrder[i] = i;
            }
            _breakingOrder.Shuffle();
        }

        private IEnumerator ShowIce()
        {
            for (int i = 0; i < _breakingOrder.Length; i++)
            {
                _ice[_breakingOrder[i]].Show();
                yield return new WaitForSeconds(_placeIceDelay);
            }
        }
    }
}
