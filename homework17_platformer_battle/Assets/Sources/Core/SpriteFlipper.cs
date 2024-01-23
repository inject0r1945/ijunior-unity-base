using UnityEngine;

namespace Platformer.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpriteFlipper : MonoBehaviour
    {
        [SerializeField] private float _zeroSpeedThreshold = 0.02f;
        [SerializeField] private bool _isInverted;

        private IVelocity _velocity;
        private SpriteRenderer _spriteRenderer;
        private float _speedX;
        private bool _isInitialized;

        private void Awake()
        {
            ValidateInitialization();
        }

        private void Update()
        {
            _speedX = _velocity.Velocity.x;

            if (_speedX <= -_zeroSpeedThreshold || _speedX >= _zeroSpeedThreshold)
            {
                if (_isInverted)
                    _spriteRenderer.flipX = _speedX > 0;
                else
                    _spriteRenderer.flipX = _speedX < 0;
            }
        }

        public void Initialize(IVelocity velocity)
        {
            _velocity = velocity;
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _isInitialized = true;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(SpriteFlipper)} is not initialized");
        }
    }
}
