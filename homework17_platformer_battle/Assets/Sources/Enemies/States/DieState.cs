using System;
using UnityEngine;

namespace Platformer.Enemies.States
{
    public class DieState : EnemyBaseState
    {
        private bool _isDied;
        private Collider2D _enemyCollider;
        private AudioSource _dieSound;

        public DieState(EnemyView enemyView, PlayerDetector playerDetector, Collider2D enemyCollider,
            AudioSource dieSound) : base(enemyView, playerDetector)
        {
            _enemyCollider = enemyCollider;
            _dieSound = dieSound;
        }

        public event Action Died;

        public override void Enter()
        {
            base.Enter();

            _enemyCollider.enabled = false;
            _dieSound.Play();
            View.PlayDieAnimation();
        }

        public override void Update()
        {
            base.Update();

            if (_isDied)
                return;

            if (View.IsPlayingDieAnimation() == false)
            {
                Died?.Invoke();
                _isDied = true;
            }
        }
    }
}