using Platformer.Capabilities;

namespace Platformer.Playing.PlayerStates
{
    public class FallState : BasePlayerState
    {
        public FallState(PlayerView playerView, Mover mover, Jumper jumper) : base(playerView, mover, jumper)
        {
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayFallAnimation();
        }
    }
}
