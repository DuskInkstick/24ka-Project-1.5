using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Movements;
using Code.Interfaces.Architecture;

namespace Code.Gameplay.Player.Character.States
{
    internal abstract class CharacterActionState : CreatureStateBase
    {
        public CharacterActionState(IStateSwitcher stateSwitcher,
                                    DirectionAnimation animation,
                                    Movement movement = null,
                                    AttackBehavior attackBehavior = null) 
            : base(stateSwitcher, animation, movement, attackBehavior) 
        { }

        public virtual void Focus(bool isFocused) { }

        public virtual void UseEscapeSkill() { }
    }
}
