using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FallSpeedLimiter : MonoBehaviour
    {
        [SerializeField, Required, MinValue(0)] private float _maxFallSpeed = 10f;

        private Rigidbody2D _rigidbody;
        private Vector2 _constraintedVelocity;
        private bool _isInitialized;

        private void Awake()
        {
            ValidateInitialization();
        }

        private void FixedUpdate()
        {
            ValidateInitialization();
            StartFallSpeedLimitBehaviour();
        }

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _constraintedVelocity = new Vector2();

            _isInitialized = true;
        }

        private void StartFallSpeedLimitBehaviour()
        {
            if (_rigidbody.velocity.y < -_maxFallSpeed)
            {
                _constraintedVelocity.x = _rigidbody.velocity.x;
                _constraintedVelocity.y = -_maxFallSpeed;

                _rigidbody.velocity = _constraintedVelocity;
            }
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(Rebound)} is not initialized");
        }
    }
}
 