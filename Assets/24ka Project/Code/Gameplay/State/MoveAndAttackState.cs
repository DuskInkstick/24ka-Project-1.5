using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Attack.AttackPatterns;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using UnityEngine;

namespace Code.Gameplay.State
{
    internal class MoveAndAttackState : MoveState, IAttacker
    {
        private AttackPattern _attackSystem;

        public MoveAndAttackState(IStateSwitcher switcher, Movement movement, FourSideAnimation animation, AttackPattern attack) 
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
