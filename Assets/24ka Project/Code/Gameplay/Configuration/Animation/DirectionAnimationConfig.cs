using Code.Gameplay.Systems.Animation;
using UnityEngine;

namespace Code.Gameplay.Configuration.Animation
{
    [CreateAssetMenu(
    fileName = "direction_animation",
    menuName = "Scriptable Objects/Direction Animation Config",
    order = 101)]
    public class DirectionAnimationConfig : ScriptableObject
    {
        public DirectionAnimationType Type = DirectionAnimationType.FourSide;

        public string Anim1 = "up_anim";
        public string Anim2 = "down_anim";
        public string Anim3 = "left_anim";
        public string Anim4 = "right_anim"; 

        public DirectionAnimation Get(Animator animator)
        {
            switch(Type)
            {
                case DirectionAnimationType.OneSide:
                    return new OneSideAnimation(animator, Anim1);

                case DirectionAnimationType.TwoSide:
                    return new TwoSideAnimation(animator, Anim1, Anim2);

                case DirectionAnimationType.FourSide:
                    return new FourSideAnimation(animator, Anim1, Anim2, Anim3, Anim4);
            }
            return null;
        }
    }


    public enum DirectionAnimationType
    {
        OneSide = 1,
        TwoSide = 2,
        FourSide = 4,
    }
}
