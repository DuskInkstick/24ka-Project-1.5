using Code.Utils;
using UnityEngine;

namespace Code.Gameplay.Player.Abilities.IceWall
{
    public class IceWall : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;

        private Animator _animator;
        private IceWallPart[] _ice;

        private int[] _breakingOrder;
        private int _breakeIndex = -1;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _ice = gameObject.GetComponentsInChildren<IceWallPart>();

            _breakingOrder = new int[_ice.Length];

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Instantiate(_hitEffect, collision.GetContact(0).point, Quaternion.identity);
            ApplyDamage(1);
        }

        private void ApplyDamage(int amount)
        {
            for (int i = 0; i < amount; i++)
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

    }
}
