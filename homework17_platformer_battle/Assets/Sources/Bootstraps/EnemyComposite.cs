using HealthVisualization.Integer;
using Pathfinding;
using Platformer.Attributes;
using Platformer.Control;
using Platformer.Core;
using Platformer.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Bootstraps
{
    public class EnemyComposite : Composite
    {
        [SerializeField, Required] private Enemy _enemy;
        [SerializeField, Required] private Collider2D _collider;
        [SerializeField, Required] private AIPath _aiPath;
        [SerializeField, Required] private Animator _animator;
        [SerializeField, Required] private SpriteFlipper _spriteFlipper;
        [SerializeField, Required] private EnemyPatrollerStateMachine _patrollerStateMachine;
        [SerializeField, Required] private PointsInitializer _patrolPointsInitializer;
        [SerializeField, Required] private Health _health;
        [SerializeField, Required] private IntSmoothHealthBar _healthbar;

        public override void Initialize()
        {
            _enemy.Initialize();
            _health.Initialize();
            _spriteFlipper.Initialize(new AIVelocity(_aiPath));
            _patrolPointsInitializer.Initialize();
            _patrollerStateMachine.Initialize(_enemy, _health, _collider, _patrolPointsInitializer.Points);

            IntHealthMediator healthMediator = new(_health);
            _healthbar.Initialize(healthMediator);
        }
    }
}
