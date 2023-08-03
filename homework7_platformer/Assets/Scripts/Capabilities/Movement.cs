using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionsDataRetriever))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Controller))]
public class Movement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

    private CollisionsDataRetriever _collisionsDataRetriever;
    private Controller _controller;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private float _directionX;
    private float _desiredVelocityX;
    private float _maxSpeedChange;
    private float _acceleration;

    private void Awake()
    {
        _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
    }

    private void Update()
    {
        _directionX = _controller.Input.RetrieveMovementInput();
        _desiredVelocityX = _directionX * Mathf.Max(_maxSpeed - _collisionsDataRetriever.Friction, 0f);
    }

    private void FixedUpdate()
    {
        _velocity = _rigidbody.velocity;
        _acceleration = _collisionsDataRetriever.OnGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocityX, _maxSpeedChange);
        _rigidbody.velocity = _velocity;
    }
}
