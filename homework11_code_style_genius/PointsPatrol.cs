using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPatrol : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _pointsParent;

    private Transform[] _patrolPoints;
    private int _patrolPointIndex;
    private Transform _destinationPatrolPoint;

    private void Start()
    {
        InitializePatrolPoints();
    }

    private void Update()
    {
        StartPatrolBehaviour();
    }
    
    private void InitializePatrolPoints()
    {
        _patrolPoints = new Transform[_pointsParent.childCount];

        for (int i = 0; i < _pointsParent.childCount; i++)
            _patrolPoints[i] = _pointsParent.GetChild(i).GetComponent<Transform>();
    }

    private void StartPatrolBehaviour()
    {
        _destinationPatrolPoint = _patrolPoints[_patrolPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, _destinationPatrolPoint.position, _moveSpeed * Time.deltaTime);

        if (transform.position == _destinationPatrolPoint.position)
        {
            CalculateNextPatrolPointIndex();
            RotateToDestination();
        }
    }
    
    private void CalculateNextPatrolPointIndex()
    {
        _patrolPointIndex++;

        if (_patrolPointIndex == _patrolPoints.Length)
            _patrolPointIndex = 0;
    }

    private void RotateToDestination()
    {
        Vector3 currentDestination = _patrolPoints[_patrolPointIndex].transform.position;
        transform.forward = currentDestination - transform.position;
    }
}