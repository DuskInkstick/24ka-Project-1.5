using UnityEngine;

namespace Code.Utils
{
    internal class OrderInLayer : MonoBehaviour
    {
        [SerializeField] private float _offset = 0;
        [SerializeField] private bool _isStatic = true;
        [SerializeField] private bool _max = false;

        private SpriteRenderer _renderer;
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            if (_max)
            {
                _renderer.sortingOrder = short.MaxValue;
                Destroy(this);
                return;
            }
            _renderer.sortingOrder = (int)(-_renderer.bounds.min.y * 10 + _offset);

            if (_isStatic)
                Destroy(this);
        }
    }
}
