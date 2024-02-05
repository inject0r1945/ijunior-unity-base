using MonoUtils;
using Platformer.Attributes;
using Platformer.Control;
using Platformer.Core;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(Health))]
    public class Vampirism : InitializedMonobehaviour, IDetector
    {
        [Title("Detection Settings")]
        [SerializeField, Required, MinValue(0)] private float _radius = 10;
        [SerializeField] private LayerMask _layerMask = ~0;

        [Title("Vampire Settings")]
        [SerializeField, Required, MinValue(0), Unit(Units.Second)] private int _oneSuckHealthSize = 1;

        [Title("Timing Settings")]
        [SerializeField, Required, MinValue(0), Unit(Units.Second)] private float _suckDelay = 2f;
        [SerializeField, Required, MinValue(0), Unit(Units.Second)] private float _maxDuration = 6f;
        [SerializeField, Required, MinValue(0), Unit(Units.Second)] private float _fullRecoveryTime = 30f;

        private InputEventer _inputEventer;
        private Transform _transform;
        private IHealing _healer;
        private MonoComponentDetector<Health> _healthDetector;
        private State _state;
        private float _currentDuration;
        private float _suckTimer;
        private float _recoveryTimer;

        public event Action Started;

        public event Action<float> Blocked;

        public event Action Recovered;

        private enum State { Run, Recovering, Stopped }

        public float DetectionRadius => _radius;

        public Transform Transform => _transform;

        public LayerMask DetectionLayers => _layerMask;

        public bool CanStart => _state == State.Stopped && _recoveryTimer == 0;

        [Inject]
        private void Construct(InputEventer inputEventer)
        {
            _inputEventer = inputEventer;
            _transform = transform;
            _healer = GetComponent<Health>();
            _healthDetector = new MonoComponentDetector<Health>(this);
            _state = State.Stopped;

            IsInitialized = true;
        }

        private void OnEnable()
        {
            _inputEventer.VampirismEnabled += OnVampirismEnable;
        }

        private void OnDisable()
        {
            _inputEventer.VampirismEnabled -= OnVampirismEnable;
        }

        private void Update()
        {
            UpdateTimers();
            StartRecoveryBehaviour();
            StartSuckLivesBehaviour();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        [Button, DisableInEditorMode]
        public bool TryStartVampirizm()
        {
            if (CanStart == false)
                return false;

            ResetTimers();

            _state = State.Run;
            Started?.Invoke();

            return true;
        }

        private void StartRecoveryBehaviour()
        {
            if (_state == State.Recovering)
            {
                if (_recoveryTimer == 0)
                {
                    Recovered?.Invoke();
                    Stop();
                }

                return;
            }

            if (_currentDuration > _maxDuration)
            {
                _recoveryTimer = _fullRecoveryTime;
                _state = State.Recovering;

                Blocked?.Invoke(_recoveryTimer);
            }
        }

        private void StartSuckLivesBehaviour()
        {
            if (_state != State.Run)
                return;

            if (_suckTimer == 0)
            {
                if (TrySuckLives())
                    _suckTimer = _suckDelay;
            }
        }

        private void ResetTimers()
        {
            _currentDuration = 0;
            _suckTimer = 0;
            _recoveryTimer = 0;
        }

        private void OnVampirismEnable()
        {
            TryStartVampirizm();
        }

        private void UpdateTimers()
        {
            _currentDuration += Time.deltaTime;
            _suckTimer = Mathf.Max(0, _suckTimer - Time.deltaTime);
            _recoveryTimer = Mathf.Max(0, _recoveryTimer - Time.deltaTime);
        }

        public bool TrySuckLives()
        {
            _healthDetector.Update();

            int suckHealthSize;
            bool isSucked = false;

            foreach (Health otherHealth in _healthDetector.DetectedComponents)
            {
                suckHealthSize = Mathf.Min(otherHealth.CurrentValue, _oneSuckHealthSize);

                isSucked |= suckHealthSize > 0;

                otherHealth.TakeDamage(suckHealthSize);
                _healer.Heal(suckHealthSize);
            }

            return isSucked;
        }

        private void Stop()
        {
            _state = State.Stopped;
        }
    }
}