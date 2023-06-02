using Code.Gameplay.Systems;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.State
{
    internal class MoveAndAttackState : MoveState, IAttacker
    {
        private AttackSystem _attackSystem;

        public MoveAndAttackState(IStateSwitcher switcher, Movement movement, FourSideAnimation animation, AttackSystem attack) 
            : base(switcher, movement, animation)
        {
            _attackSystem = attack;
        }

        public void Attack(Vector2 direction)
        {
            _attackSystem.Attack(direction);
        }
    }
}
