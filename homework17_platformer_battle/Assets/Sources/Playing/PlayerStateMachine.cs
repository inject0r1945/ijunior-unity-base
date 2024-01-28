using FiniteStateMachine;
using FiniteStateMachine.States;
using MonoUtils;
using Platformer.Attributes;
using Platformer.Capabilities;
using Platformer.Effects;
using Platformer.Playing.PlayerStates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Playing
{

    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Jumper))]
    [RequireComponent(typeof(PlayerView))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Blink))]
    public class PlayerStateMachine : InitializedMonobehaviour
    {
        [SerializeField, Required, ChildGameObjectsOnly] private AudioSource _hitSound;

        private StateMachine _stateMachine;
        private Rigidbody2D _rigidbody;
        private IState _idleState;
        private IState _runState;
        private IState _jumpState;
        private IState _doubleJumpState;
        private IState _fallState;
        private HitState _hitState;
        private DieState _dieState;
        private PlayerView _playerView;
        private Health _health;
        private Blink _playerBlink;
        private Mover _mover;
        private Jumper _jumper;
        private Animator _animator;

        private void OnEnable()
        {
            _health.Damaged += OnPlayerDamage;
            _dieState.Died += OnPlayerDieInState;
        }

        private void OnDisable()
        {
            _health.Damaged -= OnPlayerDamage;
            _dieState.Died -= OnPlayerDieInState;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        public void Initialize()
        {
            _playerView = GetComponent<PlayerView>();
            _health = GetComponent<Health>();
            _playerBlink = GetComponent<Blink>();

            _mover = GetComponent<Mover>();
            _jumper = GetComponent<Jumper>();
            _stateMachine = new StateMachine();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _idleState = new IdleState(_playerView, _mover, _jumper);
            _runState = new RunState(_playerView, _mover, _jumper);
            _jumpState = new JumpState(_playerView, _mover, _jumper);
            _doubleJumpState = new DoubleJumpState(_playerView, _mover, _jumper);
            _fallState = new FallState(_playerView, _mover, _jumper);
            _hitState = new HitState(_playerView, _mover, _jumper, _playerBlink, _hitSound);
            _dieState = new DieState(_playerView, _mover, _jumper);

            SetupStateMachine();

            IsInitialized = true;
        }

        private void SetupStateMachine()
        {
            _stateMachine.AddAnyTransition(_runState, 
                new FunctionPredicate(() =>  _health.IsDied == false && _hitState.IsHitted == false && _mover.IsMoved && _jumper.IsJumped == false));
            
            _stateMachine.AddAnyTransition(_idleState,
                new FunctionPredicate(() => _health.IsDied == false && _hitState.IsHitted == false && _mover.IsMoved == false 
                && _jumper.IsJumped == false && _rigidbody.velocity.y == 0));

            _stateMachine.AddAnyTransition(_doubleJumpState,
                new FunctionPredicate(() => _health.IsDied == false && _hitState.IsHitted == false 
                && _jumper.IsDoubleJumped && _rigidbody.velocity.y > 0));

            _stateMachine.AddAnyTransition(_jumpState,
                new FunctionPredicate(() => _health.IsDied == false && _hitState.IsHitted == false 
                && _jumper.IsJumped && _rigidbody.velocity.y > 0));

            _stateMachine.AddAnyTransition(_fallState,
                new FunctionPredicate(() => _health.IsDied == false && _hitState.IsHitted == false && _rigidbody.velocity.y < 0));

            IPredicate alwaysFalsePredicate = new FunctionPredicate(() => false);

            _stateMachine.AddAnyTransition(_dieState, new FunctionPredicate(() => _health.IsDied));
            _stateMachine.AddAnyTransition(_hitState, alwaysFalsePredicate);

            _stateMachine.TrySetState(_idleState);
        }

        private void OnPlayerDamage()
        {
            _stateMachine.TrySetState(_hitState);
        }

        private void OnPlayerDieInState()
        {
            gameObject.SetActive(false);
        }
    }
}