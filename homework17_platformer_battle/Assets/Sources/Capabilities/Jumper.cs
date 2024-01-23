using Platformer.Control;
using Platformer.Core;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CollisionsDataRetriever))]
    public class Jumper : MonoBehaviour
    {
        [SerializeField, MinValue(0), Required] private float _jumpHeight = 3f;
        [SerializeField, MinValue(0), Required] private float _upwardGravityScale = 3f;
        [SerializeField, MinValue(0), Required] private float _downwardGravityScale = 1.7f;
        [SerializeField, MinValue(0), Required] private int _maxAirJumps = 1;
        [SerializeField, Range(0f, 0.3f)] private float _coyoteTime = 0.2f;
        [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;

        private const float ZeroSpeedThreshold = 0.02f;
        private const float DefaultGravityScale = 1f;
        private const int DoubleJumpMinPhase = 2;
        private Rigidbody2D _rigidbody;
        private CollisionsDataRetriever _collisionsDataRetriever;
        private InputReceiver _inputReceiver;
        private bool _isInitialized;
        private bool _onGround;
        private Vector2 _velocityCache;
        private int _jumpPhase;
        private bool _isDesiredJump;
        private bool _isJumpHold;
        private float _coyoteCounter;
        private float _jumpBufferCounter;

        public event Action Jumped;

        public bool IsJumped => _jumpPhase > 0;

        public bool IsDoubleJumped => _jumpPhase >= DoubleJumpMinPhase;

        private void OnEnable()
        {
            ValidateInitialization();

            _inputReceiver.Jumped += OnJump;
            _inputReceiver.JumpEnded += OnJumpEnd;
        }

        private void OnDisable()
        {

            ValidateInitialization();

            _inputReceiver.Jumped -= OnJump;
            _inputReceiver.JumpEnded -= OnJumpEnd;
        }

        private void Start()
        {
            _jumpPhase = 0;
        }
        public void Initialize(InputReceiver inputReceiver)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
            _inputReceiver = inputReceiver;

            _isInitialized = true;
        }

        public void StartJumpBehaviour()
        {
            ValidateInitialization();

            _onGround = _collisionsDataRetriever.OnGround;

            UpdateVelocityCache();

            if (IsInAirOrFalling() == false)
            {
                ResetJump();
            }
            else
            {
                UpdateCoyoteTimer();
            }

            HandleJumpInput();
            HandleJumpHoldInput();
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new Exception($"{nameof(Jumper)} is not initialized");
        }

        private void OnJump()
        {
            _isDesiredJump = true;
            _isJumpHold = true;
        }

        private void OnJumpEnd()
        {
            _isJumpHold = false;
        }

        private void UpdateVelocityCache()
        {
            _velocityCache = _rigidbody.velocity;
        }

        private bool IsInAirOrFalling()
        {
            return _onGround == false || Mathf.Abs(_rigidbody.velocity.y) > ZeroSpeedThreshold;
        }

        private void ResetJump()
        {
            _jumpPhase = 0;
            _coyoteCounter = _coyoteTime;
        }

        private void UpdateCoyoteTimer()
        {
            _coyoteCounter -= Time.deltaTime;
        }

        private void HandleJumpInput()
        {
            if (_isDesiredJump)
            {
                _isDesiredJump = false;
                _jumpBufferCounter = _jumpBufferTime;
            }
            else
            {
                _jumpBufferCounter = Mathf.Max(0, _jumpBufferCounter - Time.deltaTime);
            }

            if (_jumpBufferCounter > 0)
            {
                MakeJump();
            }
        }

        private void MakeJump()
        {
            if (_coyoteCounter > 0f || (_jumpPhase <= _maxAirJumps))
            {
                AddJumpForce(ref _velocityCache);
                _jumpPhase++;
            }
        }

        private void AddJumpForce(ref Vector2 velocity)
        {
            _jumpBufferCounter = 0f;
            _coyoteCounter = 0f;
            float jumpSpeedCoefficient = -2f;
            float jumpSpeed = Mathf.Sqrt(jumpSpeedCoefficient * Physics2D.gravity.y * _jumpHeight);
            velocity.y = jumpSpeed;

            Jumped?.Invoke();
        }

        private void HandleJumpHoldInput()
        {
            if (_isJumpHold && _rigidbody.velocity.y > ZeroSpeedThreshold)
                _rigidbody.gravityScale = _upwardGravityScale;

            else if (_isJumpHold == false || _rigidbody.velocity.y < -ZeroSpeedThreshold)
                _rigidbody.gravityScale = _downwardGravityScale;

            else
                _rigidbody.gravityScale = DefaultGravityScale;

            _rigidbody.velocity = _velocityCache;
        }
    }
}