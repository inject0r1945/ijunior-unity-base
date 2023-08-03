using Prototype.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Prototype.Control
{
    [RequireComponent(typeof(Mover))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _followSpeedFraction = 1f;

        private Transform _target;
        private Mover _mover;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (_target == null)
                return;

            _mover.MoveTo(_target.position, _followSpeedFraction);
        }

        public void Init(Transform target)
        {
            _target = target;
        }
    }
}