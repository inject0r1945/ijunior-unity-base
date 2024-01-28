using FiniteStateMachine;
using FiniteStateMachine.States;
using MonoUtils;
using Platformer.Attributes;
using Platformer.Core;
using Platformer.Enemies.States;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Enemies
{
    public class EnemyPatrollerStateMachine : InitializedMonobehaviour
    {
        [SerializeField, Required, ChildGameObjectsOnly] private AudioSource _hitAudio;
        [SerializeField, Required, ChildGameObjectsOnly] private AudioSource _dieAudio;

        private StateMachine _stateMachine = new();
        private Health _enemyHealth;
        private IState _hitState;
        private DieState _dieState;

        private void OnEnable()
        {
            _enemyHealth.Damaged += OnEnemyDamage;
            _enemyHealth.Died += OnEnemyDie;
            _dieState.Died += OnEnemyDieInState;
        }

        private void OnDisable()
        {
            _enemyHealth.Damaged -= OnEnemyDamage;
            _enemyHealth.Died -= OnEnemyDie;
            _dieState.Died -= OnEnemyDieInState;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        public void Initialize(Enemy enemy, Health enemyHealth, Collider2D enemyCollider, IEnumerable<Transform> patrolPoints)
        {
            _enemyHealth = enemyHealth;

            InitializeStateMachine(enemy, enemyCollider, patrolPoints);
            IsInitialized = true;
        }

        private void OnEnemyDamage()
        {
            _stateMachine.TrySetState(_hitState);
        }

        private void OnEnemyDie()
        {
            _stateMachine.TrySetState(_dieState);
        }

        private void OnEnemyDieInState()
        {
            Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void InitializeStateMachine(Enemy enemy, Collider2D enemyCollider, IEnumerable<Transform> patrolPoints)
        {
            PlayerDetector playerDetector = new PlayerDetector(enemy);
            Patroler patroler = new Patroler(enemy, patrolPoints);
            Follower follower = new Follower(enemy);

            IState idleState = new IdleState(enemy.View, playerDetector);
            IState chaseState = new ChaseState(enemy.View, playerDetector, follower);
            IState patrolState = new PatrolState(enemy.View, playerDetector, patroler);
            IState attackState = new AttackState(enemy.View, playerDetector, enemy);
            _hitState = new HitState(enemy.View, playerDetector, _hitAudio);
            _dieState = new DieState(enemy.View, playerDetector, enemyCollider, _dieAudio);

            IPredicate chasePredicate = new FunctionPredicate(() => playerDetector.IsDetected);
            IPredicate patrolPredicate = new FunctionPredicate(() => playerDetector.IsDetected == false);
            IPredicate attackPredicate = new FunctionPredicate(() => playerDetector.GetDistanceToPlayer(enemy.Transform) <= enemy.AttackDistance);
            IPredicate chaseFromAttackPredicate = new FunctionPredicate(() => playerDetector.GetDistanceToPlayer(enemy.Transform) > enemy.AttackDistance);
            IPredicate hitStateToChasePredicate = new FunctionPredicate(() => enemy.View.IsPlayingHitAnimation() == false);
            IPredicate alwaysFalsePredicate = new FunctionPredicate(() => false);

            _stateMachine.AddTransition(idleState, chaseState, chasePredicate);
            _stateMachine.AddTransition(chaseState, patrolState, patrolPredicate);
            _stateMachine.AddTransition(patrolState, chaseState, chasePredicate);
            _stateMachine.AddTransition(chaseState, attackState, attackPredicate);
            _stateMachine.AddTransition(attackState, chaseState, chaseFromAttackPredicate);
            _stateMachine.AddTransition(attackState, patrolState, patrolPredicate);
            _stateMachine.AddTransition(_hitState, chaseState, hitStateToChasePredicate);
            _stateMachine.AddTransition(_dieState, _dieState, alwaysFalsePredicate);
            _stateMachine.TrySetState(idleState);
        }
    }
}
