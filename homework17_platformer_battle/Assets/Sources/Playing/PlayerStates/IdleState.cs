using Platformer.Capabilities;

namespace Platformer.Playing.PlayerStates
{
    public class IdleState : BasePlayerState
    {
        public IdleState(PlayerView playerView, Mover mover, Jumper jumper) : base(playerView, mover, jumper)
        {
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayIdleAnimation();
        }
    }
}