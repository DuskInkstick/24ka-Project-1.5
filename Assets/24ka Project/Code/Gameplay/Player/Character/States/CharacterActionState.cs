
using Code.Gameplay.State;
using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Attack;
using Code.Gameplay.Systems.LifeDamage;
using Code.Interfaces.Architecture;

namespace Code.Gameplay.Player.Character.States
{
    internal abstract class CharacterActionState : CreatureStateBase
    {
        public CharacterActionState(IStateSwitcher stateSwitcher,
                                    FourSideAnimation animation,
                                    Resilience resilience,
                                    Movement movement = null,
                                    AttackBehavior attackBehavior = null) 
            : base(stateSwitcher, animation, resilience, movement, attackBehavior) 
        { }

        public virtual void Focus(bool isFocused) { }

        public virtual void UseEscapeSkill() { }
    }
}
