using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] float _jumpForce = 5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private Animator _animator;

    public bool IsGrounded => _isGrounded;

    private States State
    {
        get { return (States)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

        if (!IsGrounded)
            State = States.Jump;
    }

    private void Update()
    {
        _animator.SetFloat("AirSpeedY", _rigidbody.velocity.y);

        if (IsGrounded)
            State = States.Idle;

        if (Input.GetButton("Horizontal"))
            Run();

        if (IsGrounded == true && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Run()
    {
        if (IsGrounded)
            State = States.Run;

        Vector3 currentPosition = transform.position;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        Vector3 targetPosition = currentPosition + direction;

        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);

        _spriteRenderer.flipX = direction.x <= 0;
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}

public enum States
{
    Idle,
    Run,
    Jump
}
