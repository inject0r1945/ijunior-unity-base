using UnityEngine;
using Platformer.Core;

namespace Platformer.Playing
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        private readonly string _idleAnimationName = "idle";
        private readonly string _jumpAnimationName = "jump";
        private readonly string _runAnimationName = "run";
        private readonly string _fallAnimationName = "fall";
        private readonly string _hitAnimationName = "hit";
        private readonly string _dieAnimationName = "die";
        private readonly string _startDoubleJumpTrigger = "StartDoubleJump";
        private readonly string _endDoubleJumpTrigger = "EndDoubleJump";
        private readonly string _dieTrigger = "Die";
        private readonly string _hitTrigger = "Hit";
        private readonly int _hitAnimationLayer = 1;
        private readonly int _dieAnimationLayer = 0;
        private int _idleAnimationHash;
        private int _jumpAnimationHash;
        private int _runAnimationHash;
        private int _fallAnimationHash;
        private int _startDoubleJumpTriggerHash;
        private int _endDoubleJumpTriggerHash;
        private int _dieTriggerHash;
        private int _hitTriggerHash;

        private Animator _animator;
        private bool _isInitialized;

        private void Awake()
        {
            ValidateInitialization();
        }

        public void Initialize()
        {
            _animator = GetComponent<Animator>();

            _idleAnimationHash = Animator.StringToHash(_idleAnimationName);
            _jumpAnimationHash = Animator.StringToHash(_jumpAnimationName);
            _runAnimationHash = Animator.StringToHash(_runAnimationName);
            _fallAnimationHash = Animator.StringToHash(_fallAnimationName);
            _startDoubleJumpTriggerHash = Animator.StringToHash(_startDoubleJumpTrigger);
            _endDoubleJumpTriggerHash = Animator.StringToHash(_endDoubleJumpTrigger);
            _dieTriggerHash = Animator.StringToHash(_dieTrigger);
            _hitTriggerHash = Animator.StringToHash(_hitTrigger);

            _isInitialized = true;
        }

        public void PlayIdleAnimation()
        {
            PlayAnimation(_idleAnimationHash);
        }

        public void PlayJumpAnimation()
        {
            PlayAnimation(_jumpAnimationHash);
        }

        public void PlayRunAnimation()
        {
            PlayAnimation(_runAnimationHash);
        }

        public void PlayFallAnimation()
        {
            PlayAnimation(_fallAnimationHash);
        }

        public void PlayDoubleJumpAnimation()
        {
            SetTrigger(_startDoubleJumpTriggerHash);
        }

        public void StopDoubleJumpAnimation()
        {
            SetTrigger(_endDoubleJumpTriggerHash);
        }

        public void PlayHitAnimation()
        {
            SetTrigger(_hitTriggerHash);
        }

        public void PlayDieAnimation()
        {
            SetTrigger(_dieTriggerHash);
        }

        public bool IsPlayedHitAnimation()
        {
            return _animator.IsPlayedAnimation(_hitAnimationName, _hitAnimationLayer);
        }

        public bool IsPlayedDieAnimation()
        {
            return _animator.IsPlayedAnimation(_dieAnimationName, _dieAnimationLayer, _dieTriggerHash);
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(PlayerView)} is not initialized");
        }

        private void PlayAnimation(int animationHash)
        {
            _animator.Play(animationHash);
        }

        private void SetTrigger(int triggerHash)
        {
            _animator.SetTrigger(triggerHash);
        }
    }
}
