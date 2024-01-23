using Platformer.Core;

namespace Platformer.Enemies.States
{
    public class ChaseState : EnemyBaseState
    {
        private Follower _follower;

        public ChaseState(EnemyView enemyView, PlayerDetector playerDetector, Follower follower) : base(enemyView, playerDetector)
        {
            _follower = follower;
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayChaseAnimation();
        }

        public override void Exit()
        {
            base.Exit();

            View.StopAnimations();
            _follower.Reset();
        }

        public override void Update()
        {
            base.Update();

            if (DetectedPlayer == null)
            {
                Reset();

                return;
            }
            
            if (_follower.HasTarget == false)
                _follower.SetTarget(DetectedPlayer.Transform);

            _follower.Update();
        }

        private void Reset()
        {
            if (_follower.HasTarget)
            {
                _follower.Reset();
            }
        }
    }
}
