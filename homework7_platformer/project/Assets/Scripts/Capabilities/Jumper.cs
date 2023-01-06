using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionsDataRetriever))]
[RequireComponent(typeof(Controller))]
public class Jumper: MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _upwardMovementMultiplier = 3f;
    [SerializeField] private float _downwardMovementMultiplier = 1.7f;
    [SerializeField] private int _maxAirJumps = 1;
    [SerializeField, Range(0f, 0.3f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.3f)] private float _jumpBufferTime = 0.2f;

    private Rigidbody2D _rigidbody;
    private CollisionsDataRetriever _collisionsDataRetriever;
    private Controller _controller;
    private float _zeroSpeedThreshold = 0.02f;
    private bool _onGround;
    private float _defaultGravityScale;
    private Vector2 _velocityCache;
    private int _jumpPhase;
    private bool _isDesiredJump;
    private bool _isProcessJumping;
    private float _coyoteCounter;
    private float _jumpBufferCounter;

    private UnityEvent _madeJump = new UnityEvent();

    public bool IsProcessJumping => _isProcessJumping;

    public int JumpPhase => _jumpPhase;

    public event UnityAction MadeJump
    {
        add { _madeJump.AddListener(value); }
        remove { _madeJump.RemoveListener(value); }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
        _controller = GetComponent<Controller>();
    }

    private void Start()
    {
        _defaultGravityScale = 1f;
        _jumpPhase = 0;
    }

    private void Update()
    {
        _isDesiredJump |= _controller.Input.RetrieveJumpInput();
        _onGround = _collisionsDataRetriever.OnGround;
    }

    private void FixedUpdate()
    {
        _velocityCache = _rigidbody.velocity;

        if (_onGround && Mathf.Abs(_rigidbody.velocity.y) <= _zeroSpeedThreshold)
        {
            _jumpPhase = 0;
            _coyoteCounter = _coyoteTime;
            _isProcessJumping = false;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }

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
            AddJumpToVelocityCache();
        }

        bool isJumpHold = _controller.Input.RetrieveJumpHoldInput();

        if (isJumpHold && _rigidbody.velocity.y > _zeroSpeedThreshold)
            _rigidbody.gravityScale = _upwardMovementMultiplier;

        else if (!isJumpHold || _rigidbody.velocity.y < -_zeroSpeedThreshold)
            _rigidbody.gravityScale = _downwardMovementMultiplier;

        else
            _rigidbody.gravityScale = _defaultGravityScale;

        _rigidbody.velocity = _velocityCache;
    }

    private void AddJumpToVelocityCache()
    {
        if (_coyoteCounter > 0f || (_jumpPhase < _maxAirJumps && _isProcessJumping))
        {
            if (_isProcessJumping)
                _jumpPhase++;

            _jumpBufferCounter = 0f;
            _coyoteCounter = 0f;
            float jumpSpeedCoefficient = -2f;
            float jumpSpeed = Mathf.Sqrt(jumpSpeedCoefficient * Physics2D.gravity.y * _jumpHeight);
            _isProcessJumping = true;

            _velocityCache.y = jumpSpeed;

            _madeJump.Invoke();
        }
    }
}
