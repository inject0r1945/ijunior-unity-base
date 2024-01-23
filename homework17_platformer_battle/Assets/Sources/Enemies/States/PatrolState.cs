using Platformer.Core;

namespace Platformer.Enemies.States
{
    public class PatrolState : EnemyBaseState
    {
        private Patroler _patroler;

        public PatrolState(EnemyView enemyView, PlayerDetector playerDetector, Patroler patroler) : base(enemyView, playerDetector) 
        {
            _patroler = patroler;
        }

        public override void Enter()
        {
            base.Enter();

            _patroler.Run();
            View.PlayPatrolAnimation();
        }

        public override void Exit()
        {
            base.Exit();

            View.StopAnimations();
            _patroler.Stop();
        }

        public override void Update()
        {
            base.Update();

            _patroler.Update();
        }
    }
}