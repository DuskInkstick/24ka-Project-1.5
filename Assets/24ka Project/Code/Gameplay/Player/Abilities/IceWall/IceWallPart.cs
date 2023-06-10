using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Player.Abilities.IceWall
{
    public class IceWallPart : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private int _iceSize;
        public int IceSize => _iceSize;

        private int _spriteIndex = 0;
        public int SpriteIndex 
        {
            get => _spriteIndex;
            set 
            {
                if (value >= 0 && value <= 4)
                    _spriteIndex = value;
            }
        }

        private bool _isBroken = false;
        public bool IsBroken { get => _isBroken; set => SetBroken(value); }

        private Animator _animator;

        private Dictionary<int, string> _animations = new()
        {
            {0, "ice5_normal" },
            {1, "ice6_normal" },
            {2, "ice7_normal" },
            {3, "ice8_normal" },
            {4, "ice9_normal" },
        };

        public void Show() => _animator.Play(_animations[_spriteIndex]);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            SetBroken(false);
        }
        
        private void SetBroken(bool value) => _animator.SetBool("IsBroken", value);
    }
}
