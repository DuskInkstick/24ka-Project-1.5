using Code.Gameplay.Systems.Battle.Actions;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using System;

namespace Code.Utils
{
    internal static class EnumsHelper
    {
        private static Random _random = new Random();
        public static int Next(this LoopMode loopMode, int current, int max)
        {
            switch (loopMode)
            {
                case LoopMode.Loop:
                    return (current + 1) % max;

                case LoopMode.Random:
                    return _random.Next(0, max);
            }
            return 0;
        }

        public static DamagingAction GetDamagingAction(
            this AttackingActionType actionType,
            int damage,
            ElementalAttribute attribute)
        {
            switch (actionType)
            {
                case AttackingActionType.Normal:
                    return new DamagingAction(damage, attribute);

                case AttackingActionType.Penetrated:
                   return new PenetratedDamagingAction(damage, attribute);

                case AttackingActionType.Constant:
                    return new ConstantDamagingAction(damage, attribute);
            }
            return null;
        }
    }
}
