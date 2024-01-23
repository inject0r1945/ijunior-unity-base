using Platformer.Capabilities;

namespace Platformer.Playing.PlayerStates
{
    public class DoubleJumpState : BasePlayerState
    {
        public DoubleJumpState(PlayerView playerView, Mover mover, Jumper jumper) : base(playerView, mover, jumper)
        {
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayDoubleJumpAnimation();
        }

        public override void Exit()
        {
            base.Exit();

            View.StopDoubleJumpAnimation();
        }
    }
}
