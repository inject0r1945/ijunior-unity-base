namespace Platformer.Enemies.States
{
    public class IdleState : EnemyBaseState
    {
        public IdleState(EnemyView enemyView, PlayerDetector playerDetector) : base(enemyView, playerDetector)
        {
        }

        public override void Enter()
        {
            base.Enter();

            View.PlayIdleAnimation();
        }

        public override void Exit()
        {
             base.Exit();

            View.StopAnimations();
        }
    }
}