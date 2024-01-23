using Platformer.Control;
using Platformer.Core;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(CollisionsDataRetriever))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        [SerializeField, MinValue(0), Required] private float _maxSpeed = 7f;
        [SerializeField, MinValue(0), Required] private float _maxAcceleration = 40f;
        [SerializeField, MinValue(0), Required] private float _maxAirAcceleration = 20f;

        private CollisionsDataRetriever _collisionsDataRetriever;
        private InputReceiver _inputReceiver;
        private bool _isInitialized;
        private Rigidbody2D _rigidbody;
        private Vector2 _velocity;
        private float _directionX;
        private float _desiredVelocityX;
        private float _maxSpeedChange;
        private float _acceleration;

        public bool IsMoved => Mathf.Approximately(_desiredVelocityX, 0) == false;

        private void OnEnable()
        {
            ValidateInitialization();

            _inputReceiver.Moved += OnMove;
        }

        private void OnDisable()
        {
            ValidateInitialization();

            _inputReceiver.Moved -= OnMove;
        }

        public void Initialize(InputReceiver inputReceiver)
        {
            _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputReceiver = inputReceiver;

            _isInitialized = true;
        }

        public void StartMoveBehaviour()
        {
            ValidateInitialization();

            _velocity = _rigidbody.velocity;
            _acceleration = _collisionsDataRetriever.OnGround ? _maxAcceleration : _maxAirAcceleration;
            _maxSpeedChange = _acceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocityX, _maxSpeedChange);
            _rigidbody.velocity = _velocity;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(Mover)} is not initialized");
        }

        private void OnMove(float directionX)
        {
            _directionX = directionX;
            _desiredVelocityX = _directionX * Mathf.Max(_maxSpeed - _collisionsDataRetriever.Friction, 0f);
        }
    }
}
