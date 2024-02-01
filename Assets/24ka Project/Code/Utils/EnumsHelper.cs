using Code.Gameplay.Systems.Battle.Actions;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using System;

namespace Code.Utils
{
    internal static class EnumsHelper
    {
        private static Random _random = new Random();
        public static int Next(this SelectionMode loopMode, int current, int max)
        {
            switch (loopMode)
            {
                case SelectionMode.Loop:
                    return (current + 1) % max;

                case SelectionMode.Random:
                    return _random.Next(0, max);
            }
            return 0;
        }

        public static CausingDamage GetDamagingAction(
            this CausingDamageType actionType,
            int damage,
            ElementalAttribute attribute)
        {
            switch (actionType)
            {
                case CausingDamageType.Normal:
                    return new CausingDamage(damage, attribute);

                case CausingDamageType.Penetrating:
                   return new CausingPenetratingDamage(damage, attribute);

                case CausingDamageType.Constant:
                    return new CausingConstantDamage(damage, attribute);
            }
            return null;
        }
    }
}
