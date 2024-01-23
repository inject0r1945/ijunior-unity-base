using FiniteStateMachine.States;
using Platformer.Capabilities;

namespace Platformer.Playing.PlayerStates
{
    public class BasePlayerState : BaseState
    {
        private PlayerView _playerView;
        private Mover _mover;
        private Jumper _jumper;

        public BasePlayerState(PlayerView playerView, Mover mover, Jumper jumper)
        {
            _playerView = playerView;
            _mover = mover;
            _jumper = jumper;
        }

        public PlayerView View => _playerView;

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _jumper.StartJumpBehaviour();
            _mover.StartMoveBehaviour();
        }
    }
}
