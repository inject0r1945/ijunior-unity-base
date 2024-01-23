using Platformer.Attributes;
using Platformer.Core;
using UnityEngine;

namespace Platformer.Enemies.States
{
    public class AttackState : EnemyBaseState
    {
        private IDamager _damager;
        private float _attackTimer;
        private IDamageable[] _damageables;

        public AttackState(EnemyView enemyView, PlayerDetector playerDetector, IDamager damager) : base(enemyView, playerDetector)
        {
            _damager = damager;
            Reset();
        }

        public override void Enter()
        {
            base.Enter();

            Reset();
            View.PlayAttackAnimation();
        }

        public override void Exit()
        {
            base.Exit();

            View.StopAnimations();
            Reset();
        }

        public override void Update()
        {
            base.Update();

            if (DetectedPlayer == null)
                Reset();

            if (_attackTimer >= _damager.AttackDelay)
                Attack();

            UpdateTimers();
        }

        private void Reset()
        {
            _attackTimer = _damager.AttackDelay;
            _damageables = null;
        }

        private void Attack()
        {
            if (DetectedPlayer == null)
                return;

            if (_damageables == null)
                _damageables = DetectedPlayer.Transform.GetComponents<IDamageable>();

            foreach (IDamageable damageable in _damageables)
                damageable.TakeDamage(_damager.Damage);

            _attackTimer = 0;
        }

        private void UpdateTimers()
        {
            _attackTimer += Time.deltaTime;
        }
    }
}