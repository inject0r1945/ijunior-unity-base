using HealthVisualization;
using MonoUtils;
using Sirenix.OdinInspector;
using Specifications;
using System;
using UnityEngine;

namespace Platformer.Attributes
{
    public class Health : InitializedMonobehaviour, IDamageable, IHealing, IHealth<int>
    {
        [SerializeField, Required, MinValue(0)] private int _maxValue = 3;

        private const int MinValue = 0;
        private int _currentValue;
        private bool _isEnableDamage = true;
        private ISpecification<int> _damageSpecification;
        private ISpecification<int> _healthSpecification;

        public event Action Damaged;

        public event Action<int> Changed;

        public event Action Died;

        public event Action Healed;

        public int MaxValue => _maxValue;

        [ShowInInspector, ReadOnly]
        public int CurrentValue
        {
            get => _currentValue;

            private set
            {
                _currentValue = value;

                if (!_healthSpecification.IsSatisfiedBy(_currentValue))
                    _currentValue = MinValue;

                if (_currentValue > MaxValue)
                    _currentValue = MaxValue;
            }
        }

        public float Percent => CurrentValue / (float)MaxValue;

        public bool IsDied => CurrentValue == 0;

        private void Start()
        {
            SendChangeEvent();
        }

        public void Initialize()
        {
            _damageSpecification = new IntGreatOrEqualZeroSpecification();
            _healthSpecification = new IntGreatOrEqualZeroSpecification();

            CurrentValue = _maxValue;

            IsInitialized = true;
        }

        public void TakeDamage(int damage)
        {
            if (IsDied)
                return;

            if (_isEnableDamage == false)
                return;

            if (_damageSpecification.IsSatisfiedBy(damage) == false)
                throw new Exception("Damage value does not match specification");

            if (damage == 0)
                return;

            CurrentValue -= damage;

            SendChangeEvent();
            Damaged?.Invoke();

            if (CurrentValue == 0)
                Died?.Invoke();
        }

        public void Heal(int value)
        {
            if (_damageSpecification.IsSatisfiedBy(value) == false)
                throw new Exception("Heal value does not match specification");

            CurrentValue += value;

            SendChangeEvent();
            Healed?.Invoke();
        }

        public void DisableDamage()
        {
            _isEnableDamage = true;
        }

        public void EnableDamage()
        {
            _isEnableDamage = false;
        }

        private void SendChangeEvent()
        {
            Changed?.Invoke(CurrentValue);
        }
    }
}
