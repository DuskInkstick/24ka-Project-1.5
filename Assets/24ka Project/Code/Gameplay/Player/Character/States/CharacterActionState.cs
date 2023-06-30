using Code.Gameplay.State;
using Code.Gameplay.Systems;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;

namespace Code.Gameplay.Player.Character.States
{
    internal abstract class CharacterActionState : CreatureStateBase
    {
        public CharacterActionState(IStateSwitcher stateSwitcher,
                                    FourSideAnimation animation,
                                    Movement movement = null,
                                    AttackBehavior attackBehavior = null) 
            : base(stateSwitcher, animation, movement, attackBehavior) 
        { }

        public virtual void Focus(bool isFocused) { }

        public virtual void UseEscapeSkill() { }
    }
}
