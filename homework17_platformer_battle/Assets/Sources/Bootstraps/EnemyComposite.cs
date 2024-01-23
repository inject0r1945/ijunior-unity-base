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
        [SerializeField, Required] private Collider2D _enemyCollider;
        [SerializeField, Required] private AIPath _enemyAIPath;
        [SerializeField, Required] private Animator _enemyAnimator;
        [SerializeField, Required] private SpriteFlipper _enemySpriteFlipper;
        [SerializeField, Required] private EnemyPatrolerStateMachine _enemyStateMachine;
        [SerializeField, Required] private PointsInitializer _patrolPointsInitializer;
        [SerializeField, Required] private Health _enemyHealth;

        public override void Initialize()
        {
            _enemy.Initialize();
            _enemyHealth.Initialize();
            _enemySpriteFlipper.Initialize(new AIVelocity(_enemyAIPath));
            _patrolPointsInitializer.Initialize();
            _enemyStateMachine.Initialize(_enemy, _enemyHealth, _enemyCollider, _patrolPointsInitializer.Points);
        }
    }
}
