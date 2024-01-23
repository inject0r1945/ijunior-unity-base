using UnityEngine;

namespace Platformer.Core
{
    public static class AnimatorExtension
    {
        public static bool IsPlayedAnimation(this Animator animator, string name, int layerIndex, int triggerHash)
        {
            bool isSetDieTrigger = animator.GetBool(triggerHash);

            return animator.IsPlayedAnimation(name, layerIndex) || isSetDieTrigger;
        }

        public static bool IsPlayedAnimation(this Animator animator, string name, int layerIndex)
        {
            bool isPlayAnimation = animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);

            return isPlayAnimation;
        }
    }
}