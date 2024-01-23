using Platformer.Capabilities;

namespace Platformer.Playing.PlayerStates
{
    public class RunState : BasePlayerState
    {
        public RunState(PlayerView playerView, Mover mover, Jumper jumper) : base(playerView, mover, jumper)
        {
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayRunAnimation();
        }
    }
}