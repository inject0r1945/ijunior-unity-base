using Platformer.Capabilities;

namespace Platformer.Playing.PlayerStates
{
    public class JumpState : BasePlayerState
    {
        public JumpState(PlayerView playerView, Mover mover, Jumper jumper) : base(playerView, mover, jumper)
        {
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayJumpAnimation();
        }
    }
}
