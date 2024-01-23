using UnityEngine;

namespace Platformer.Enemies.States
{
    public class HitState : EnemyBaseState
    {
        private AudioSource _hitAudio;

        public HitState(EnemyView enemyView, PlayerDetector playerDetector, AudioSource hitAudio) : base(enemyView, playerDetector)
        {
            _hitAudio = hitAudio;
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayHitAnimation();
            _hitAudio.Play();
        }
    }
}
