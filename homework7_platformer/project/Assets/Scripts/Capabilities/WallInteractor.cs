using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionsDataRetriever))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Controller))]
public class WallInteractor : MonoBehaviour
{
    [Header("Wall Slide")]
    [SerializeField, Range(0.1f, 5f)] private float _wallSlideMaxSpeed = 2f;

    [Header("Wall Jump")]
    [SerializeField] private Vector2 _wallJumpClimb = new Vector2(4f, 12f);
    [SerializeField] private Vector2 _wallJumpBounce = new Vector2(10.7f, 10f);

    private CollisionsDataRetriever _collisionsDataRetriever;
    private Rigidbody2D _rigidbody;
    private Controller _controller;

    private Vector2 _velocity;
    private bool _onWall;
    private bool _onGround;
    private bool _isDesiredJump;
    private float _wallDirectionX;
    private bool _isWallJumping;
    private float _currentBounceDistance;

    private void Awake()
    {
        _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
    }

    private void Update()
    {
        if (_onWall && !_onGround)
        {
            _isDesiredJump |= _controller.Input.RetrieveJumpInput();
        }
    }

    private void FixedUpdate()
    {
        _velocity = _rigidbody.velocity;
        _onWall = _collisionsDataRetriever.OnWall;
        _onGround = _collisionsDataRetriever.OnGround;
        _wallDirectionX = _collisionsDataRetriever.ContactNormal.x;

        #region Wall Slide
         if (_onWall && _velocity.y < -_wallSlideMaxSpeed)
        {
            _velocity.y = -_wallSlideMaxSpeed;
        }
        #endregion

        #region Wall Jump
        if ((_onGround && _velocity.x == 0) || _onGround)
        {
            _isWallJumping = false;
        }

        if (_isDesiredJump)
        {
            float currentMovementInput = _controller.Input.RetrieveMovementInput();

            if (Mathf.Round(-_wallDirectionX) == currentMovementInput)
            {
                _velocity = new Vector2(_wallJumpClimb.x * _wallDirectionX, _wallJumpClimb.y);
            }
            else
            {
                _velocity = new Vector2(_wallJumpBounce.x * _wallDirectionX, _wallJumpBounce.y);
            }

            _isWallJumping = true;
            _isDesiredJump = false;
        }
        #endregion

        _rigidbody.velocity = _velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionsDataRetriever.CalculateCollisionLocations(collision);

        if (_collisionsDataRetriever.OnWall && !_collisionsDataRetriever.OnGround && _isWallJumping)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
