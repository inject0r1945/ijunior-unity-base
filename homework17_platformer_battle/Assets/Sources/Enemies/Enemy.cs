using Pathfinding;
using Platformer.Attributes;
using Platformer.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Enemies
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(EnemyView))]
    public class Enemy : MonoBehaviour, IDetector, IAIMovable, IDamager
    {
        [Title("Move Settings")]
        [SerializeField, Required, MinValue(0)] private float _speed = 1.3f;

        [Title("Player Detection Settings")]
        [SerializeField, Required, MinValue(0)] private float _playerDetectionRadius = 1f;

        [Title("Attack Settings")]
        [SerializeField, Required, MinValue(0)] private float _attackDistance = 1f;
        [SerializeField, Required, MinValue(0)] private int _damage = 1;
        [SerializeField, Required, MinValue(0)] private float _attackDelay = 1;

        private Health _health;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private bool _isInitialized;
        private AIPath _pathfinder;
        private EnemyView _view;

        public float DetectionRadius => _playerDetectionRadius;

        public Transform Transform => _transform;

        public AIPath Pathfinder => _pathfinder;

        public float Speed => _speed;

        public int Damage => _damage;

        public float AttackDistance => _attackDistance;

        public float AttackDelay => _attackDelay;

        public EnemyView View => _view;

        private void Awake()
        {
            ValidateInitialization();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_playerDetectionRadius, _playerDetectionRadius, 0));
        }

        public void Initialize()
        {
            _health = GetComponent<Health>();
            _transform = transform;
            _pathfinder = GetComponent<AIPath>();
            _view = GetComponent<EnemyView>();
            _view.Initialize();

            _isInitialized = true;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(Enemy)} is not initialized");
        }
    }
}