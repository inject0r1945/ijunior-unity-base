using UnityEngine;
using Platformer.Core;
using Sirenix.OdinInspector;
using MonoUtils;

namespace Platformer.Enemies
{
   [RequireComponent(typeof(Animator))]
    public class EnemyView : InitializedMonobehaviour
    {
        [SerializeField, Required, AssetsOnly] private AnimatorOverrideController _animatorOverrider;

        private readonly string _idleAnimationName = "idle";
        private readonly string _chaseAnimationName = "chase";
        private readonly string _partolAnimationName = "patrol";
        private readonly string _hitAnimationTrigger = "Hit";
        private readonly string _hitAnimationName = "hit";
        private readonly string _dieAnimationTrigger = "Die";
        private readonly string _dieAnimationName = "die";
        private readonly int _animationLayerIndex = 0;
        private int _idleAnimationHash;
        private int _patrolAnimationHash;
        private int _chaseAnimationHash;
        private int _dieAnimationTriggerHash;
        private int _hitAnimationTriggerHash;
        private Animator _animator;

        public void Initialize()
        {
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = _animatorOverrider;

            _idleAnimationHash = Animator.StringToHash(_idleAnimationName);
            _chaseAnimationHash = Animator.StringToHash(_chaseAnimationName);
            _patrolAnimationHash = Animator.StringToHash(_partolAnimationName);
            _hitAnimationTriggerHash = Animator.StringToHash(_hitAnimationTrigger);
            _dieAnimationTriggerHash = Animator.StringToHash(_dieAnimationTrigger);

            IsInitialized = true;
        }

        public void PlayIdleAnimation()
        {
            _animator.Play(_idleAnimationHash);
        }

        public void StopAnimations()
        {
            _animator.StopPlayback();
        }

        public void PlayChaseAnimation()
        {
            _animator.Play(_chaseAnimationHash);
        }

        public void PlayPatrolAnimation()
        {
            _animator.Play(_patrolAnimationHash);
        }

        public void PlayAttackAnimation()
        {
            PlayChaseAnimation();
        }

        public void PlayHitAnimation()
        {
            SetTrigger(_hitAnimationTriggerHash);
        }

        public void PlayDieAnimation()
        {
            SetTrigger(_dieAnimationTriggerHash);
        }

        public bool IsPlayingHitAnimation()
        {
            return _animator.IsPlayedAnimation(_hitAnimationName, _animationLayerIndex, _hitAnimationTriggerHash);
        }

        public bool IsPlayingDieAnimation()
        {
            return _animator.IsPlayedAnimation(_dieAnimationName, _animationLayerIndex, _dieAnimationTriggerHash);
        }

        private void SetTrigger(int triggerHash)
        {
            if (_animator.GetBool(triggerHash))
                return;

            _animator.SetTrigger(triggerHash);
        }
    }
}
