using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Movement _movement;
    private bool _isGrounded;
    private readonly string _animatorStateName = "State";
    private int _animatorStateHash;
    private readonly string _animatorAirSpeedName = "AirSpeedY";
    private int _animatorAirSpeedHash;

    public bool IsGrounded => _isGrounded;

    private States AnimationState
    {
        get { return (States)_animator.GetInteger(_animatorStateHash); }
        set { _animator.SetInteger(_animatorStateHash, (int)value); }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = GetComponent<Movement>();
        _animatorStateHash = Animator.StringToHash(_animatorStateName);
        _animatorAirSpeedHash = Animator.StringToHash(_animatorAirSpeedName);
    }

    private void FixedUpdate()
    {
        DetectGroundUnderPlayer();
    }

    private void DetectGroundUnderPlayer()
    {
        float overlapCircleRadius = 0.2f;
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, overlapCircleRadius);

        _isGrounded = overlappedColliders.Length > 1;
    }

    private void Update()
    {
        _animator.SetFloat(_animatorAirSpeedHash, _rigidbody.velocity.y);
        AnimationState = _movement.CurrentState;
        _spriteRenderer.flipX = !(_movement.CurrentDirection == Directions.Forward);
    }
}

