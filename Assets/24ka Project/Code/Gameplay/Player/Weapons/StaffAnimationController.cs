using UnityEngine;

namespace Code.Gameplay.Player.Weapons
{
    internal class StaffAnimationController : MonoBehaviour
    {
        private Animator _animator;

        public void PlayAttack()
        {
            _animator.SetBool("IsAttacking", true);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAttackFinished()
        {
            _animator.SetBool("IsAttacking", false);
        }
    }
}
