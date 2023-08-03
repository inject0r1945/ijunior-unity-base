using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(CollisionsDataRetriever))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Controller _controller;
    private Jumper _jumper;
    private CollisionsDataRetriever _collisionsDataRetriever;
    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;

    private float zeroSpeedThreshold = 0.02f;
    private int _rightWallNormal = 1;
    private bool _isDiedPlayer;

    private readonly string _animatorStateName = "State";
    private readonly string _animatorAirSpeedYName = "AirSpeedY";
    private readonly string _animatorDoubleJumpTriggerName = "DoubleJump";
    private readonly string _animatorHitTriggerName = "Hit";
    private readonly string _animatorDieTriggerName = "Die";
    private readonly string _animatorBoolWallName = "OnWall";

    private int _animatorStateHash;
    private int _animatorAirSpeedYHash;
    private int _animatorDoubleJumpTriggerHash;
    private int _animatorHitTriggerHash;
    private int _animatorDieTriggerHash;
    private int _animatorBoolWallHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _controller = GetComponent<Controller>();
        _jumper = GetComponent<Jumper>();
        _collisionsDataRetriever = GetComponent<CollisionsDataRetriever>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();

        _animatorStateHash = Animator.StringToHash(_animatorStateName);
        _animatorAirSpeedYHash = Animator.StringToHash(_animatorAirSpeedYName);
        _animatorDoubleJumpTriggerHash = Animator.StringToHash(_animatorDoubleJumpTriggerName);
        _animatorHitTriggerHash = Animator.StringToHash(_animatorHitTriggerName);
        _animatorDieTriggerHash = Animator.StringToHash(_animatorDieTriggerName);
        _animatorBoolWallHash = Animator.StringToHash(_animatorBoolWallName);
    }

    private void OnEnable()
    {
        _jumper.MadeJump += OnJumpMade;
        _playerHealth.TookDamage += OnTookDamage;
        _playerHealth.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        _jumper.MadeJump -= OnJumpMade;
        _playerHealth.TookDamage -= OnTookDamage;
        _playerHealth.PlayerDied -= OnPlayerDied;
    }

    private void Update()
    {
        if (_isDiedPlayer)
            return;

        _animator.SetFloat(_animatorAirSpeedYHash, _rigidbody.velocity.y);

        SwitchSpriteDirection();

        CalculateIddleAnimation();
        CalculateRunAnimation();
        CalculateJumpWallAnimation();
        CalculateJumpAnimation();
    }

    private void OnJumpMade()
    {
        _animator.SetInteger(_animatorStateName, (int)AnimationState.Jump);

        if (_jumper.JumpPhase > 0 && !_collisionsDataRetriever.OnWall)
            _animator.SetTrigger(_animatorDoubleJumpTriggerHash);
    }

    private void OnTookDamage()
    {
        _animator.SetTrigger(_animatorHitTriggerHash);
    }

    private void OnPlayerDied()
    {
        _animator.SetTrigger(_animatorDieTriggerHash);
    }

    private void SwitchSpriteDirection()
    {
        float movementDirection = _controller.Input.RetrieveMovementInput();
        bool isHorizontalMovement = Mathf.Abs(_rigidbody.velocity.x) > zeroSpeedThreshold;

        if (_collisionsDataRetriever.OnWall && !_collisionsDataRetriever.OnGround)
            _spriteRenderer.flipX = Mathf.Round(_collisionsDataRetriever.ContactNormal.x) == _rightWallNormal;

        else if (movementDirection != 0)
            _spriteRenderer.flipX = movementDirection < 0f;

        else if (isHorizontalMovement)
            _spriteRenderer.flipX = _rigidbody.velocity.x < -zeroSpeedThreshold;
    }

    private void CalculateIddleAnimation()
    {
        if (_collisionsDataRetriever.OnGround && Mathf.Abs(_rigidbody.velocity.y) <= zeroSpeedThreshold
            && Mathf.Abs(_rigidbody.velocity.x) <= zeroSpeedThreshold)
            _animator.SetInteger(_animatorStateHash, (int)AnimationState.Idle);
    }

    private void CalculateRunAnimation()
    {
        if (_collisionsDataRetriever.OnGround && Mathf.Abs(_rigidbody.velocity.x) > zeroSpeedThreshold
            && !_collisionsDataRetriever.OnWall && _rigidbody.velocity.y < zeroSpeedThreshold)
        {
            _animator.SetInteger(_animatorStateHash, (int)AnimationState.Run);
        }
    }

    private void CalculateJumpWallAnimation()
    {
        bool isEnableWallAnimation = _collisionsDataRetriever.OnWall && !_collisionsDataRetriever.OnGround;
        _animator.SetBool(_animatorBoolWallHash, isEnableWallAnimation);
    }

    private void CalculateJumpAnimation()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) > zeroSpeedThreshold)
            _animator.SetInteger(_animatorStateName, (int)AnimationState.Jump);
    }
}

enum AnimationState
{
    Idle,
    Run,
    Jump
}
