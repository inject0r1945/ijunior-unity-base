using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody2D _playerRigidbody;
    private States _currentState;
    private Directions _currentDirection;
    private float _previousXPosition;

    public States CurrentState => _currentState;
    public Directions CurrentDirection => _currentDirection;

    private void Awake()
    {
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _previousXPosition = _player.transform.position.x;
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetButtonDown("Jump"))
            Jump();

        float positionXOffset = _player.transform.position.x - _previousXPosition;

        if (_player.IsGrounded && positionXOffset == 0 
            && _playerRigidbody.velocity == Vector2.zero)
            _currentState = States.Idle;

        _previousXPosition = _player.transform.position.x;
    }

    private void Run()
    {
        if (_player.IsGrounded && _playerRigidbody.velocity.y == 0)
            _currentState = States.Run;

        Vector3 currentPosition = transform.position;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        Vector3 targetPosition = currentPosition + direction;

        _currentDirection = (direction.x >= 0) ? Directions.Forward : Directions.Backward;

        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_player.IsGrounded)
        {
            _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _currentState = States.Jump;
        }
    }
}

public enum States
{
    Idle,
    Run,
    Jump
}

public enum Directions
{
    Forward,
    Backward
}
