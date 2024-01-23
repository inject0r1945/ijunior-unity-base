using Platformer.Capabilities;
using System;

namespace Platformer.Playing.PlayerStates
{
    public class DieState : BasePlayerState
    {
        public DieState(PlayerView playerView, Mover mover, Jumper jumper) : base(playerView, mover, jumper)
        {
        }

        public event Action Died;

        public override void Enter()
        {
            base.Enter();

            View.PlayDieAnimation();
        }

        public override void Update()
        {
            if (View.IsPlayedDieAnimation() == false)
                Die();
        }

        private void Die()
        {
            Died?.Invoke();
        }
    }
}
