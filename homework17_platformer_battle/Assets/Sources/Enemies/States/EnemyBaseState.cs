using FiniteStateMachine.States;
using Platformer.Core;

namespace Platformer.Enemies.States
{
    public class EnemyBaseState : BaseState
    {
        private PlayerDetector _playerDetector;
        private EnemyView _enemyView;

        public EnemyBaseState(EnemyView enemyView, PlayerDetector playerDetector)
        {
            _playerDetector = playerDetector;
            _enemyView = enemyView;
        }

        public EnemyView View => _enemyView;

        protected ITransform DetectedPlayer => _playerDetector.DetectedPlayer;

        public override void Update()
        {
            _playerDetector.Update();
        }
    }
}