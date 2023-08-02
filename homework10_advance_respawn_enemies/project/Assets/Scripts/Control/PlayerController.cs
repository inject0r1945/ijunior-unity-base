using Prototype.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Prototype.Control
{
    [RequireComponent(typeof(Mover))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PatrolPath _patrolPath;
        [SerializeField] private float _waypointTolerance = 1f;
        [SerializeField] private float _waypointDwellTime = 3f;
        [SerializeField, Range(0, 1)] private float _patrolSpeedFraction = 0.2f;

        private Mover _mover;
        private int _currentWaypointIndex;
        private float _timeSinceArrivedAtWaypoint;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            StartPatrolBehaviour();
            UpdateTimers();
        }

        private void StartPatrolBehaviour()
        {
            Vector3 nextPosition = transform.position;

            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    SetNextWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > _waypointDwellTime)
            {
                _mover.MoveTo(nextPosition, _patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());

            return distanceToWaypoint < _waypointTolerance;
        }

        private void SetNextWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextWaypointIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void UpdateTimers()
        {
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }
    }
}