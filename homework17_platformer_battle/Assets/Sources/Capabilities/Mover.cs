using MonoUtils;
using Platformer.Control;
using Platformer.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(CollisionsDataRetriever))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : InitializedMonobehaviour
    {
        [SerializeField, MinValue(0), Required] private float _maxSpeed = 7f;
        [SerializeField, MinValue(0), Required] private float _maxAcceleration = 40f;
        [SerializeField, MinValue(0), Required] private float _maxAirAcceleration = 20f;

        private CollisionsDataRetriever _collisionsDataRetriever;
        private InputEventer _inputEventer;
        private Rigidbody2D _rigidbody;
        private Vector2 _velocity;
        private float _directionX;
        private float _desiredVelocityX;
        private float _maxSpeedChange;
        private float _acceleration;

        public bool IsMoved => Mathf.Approximately(_desiredVelocityX, 0) == false;

        private void OnEnable()
        {
            _inputEventer.Moved += OnMove;
        }

        private void OnDisable()
        {
            _inputEventer.Moved -= OnMove;
        }

        public void Initialize(InputEventer inputEventer)
        {
            _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputEventer = inputEventer;

            IsInitialized = true;
        }

        public void StartMoveBehaviour()
        {
            _velocity = _rigidbody.velocity;
            _acceleration = _collisionsDataRetriever.OnGround ? _maxAcceleration : _maxAirAcceleration;
            _maxSpeedChange = _acceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocityX, _maxSpeedChange);
            _rigidbody.velocity = _velocity;
        }

        private void OnMove(float directionX)
        {
            _directionX = directionX;
            _desiredVelocityX = _directionX * Mathf.Max(_maxSpeed - _collisionsDataRetriever.Friction, 0f);
        }
    }
}
