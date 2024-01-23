using Platformer.Capabilities;
using Platformer.Effects;
using UnityEngine;

namespace Platformer.Playing.PlayerStates
{
    public class HitState : BasePlayerState
    {
        private Blink _playerBlink;
        private AudioSource _hitAudio;

        public HitState(PlayerView playerView, Mover mover, Jumper jumper,
            Blink playerBlink, AudioSource hitAudio) : base(playerView, mover, jumper)
        {
            _playerBlink = playerBlink;
            _hitAudio = hitAudio;
        }

        public bool IsHitted { get; private set; }

        public override void Enter()
        {
            base.Enter();

            View.PlayHitAnimation();
            _playerBlink.StartBlink();
            _hitAudio.Play();
            IsHitted = true;
        }

        public override void Update()
        {
            base.Update();
            IsHitted = View.IsPlayedHitAnimation();
        }
    }
}
