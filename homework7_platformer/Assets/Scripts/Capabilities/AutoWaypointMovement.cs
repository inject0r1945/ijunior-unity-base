using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWaypointMovement : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _stepsCount = 0;
    [SerializeField, Range(0, 100)] private float _stepSize = 1f;
    [SerializeField, Range(0, 100)] private float _speed = 4f;
    [SerializeField] private bool _isHorizontal = true;
    [SerializeField] private bool _isCentered = true;
    [SerializeField] private bool _isReversed = false;
    [SerializeField] private GameObject _pointPrefab;

    private Vector3 _startingPosition;
    private Vector3 _step;
    private List<Vector3> _points = new List<Vector3>();
    private int _currentTargetPointNumber;
    private Vector3 _targetPoint;

    private void Awake()
    {
        _startingPosition = transform.position;
        _step = Vector3.zero;

        if (_isHorizontal)
            _step.x = _stepSize;
        else
            _step.y = _stepSize;
    }

    private void Start()
    {
        if (_isReversed)
            _step = -_step;

        if (_isCentered)
        {
            int halfCoefficient = 2;
            int halfStepsCount = _stepsCount / halfCoefficient;

            _startingPosition = _startingPosition - _step * halfStepsCount;
        }

        for (int stepNumber = 0; stepNumber <= _stepsCount; stepNumber++)
        {
            Vector3 newPoint = _startingPosition + _step * stepNumber;
            _points.Add(newPoint);
        }

        InstantiatePointsPrefabs();
    }

    private void Update()
    {
        if (_points.Count == 0)
            return;

        _targetPoint = _points[_currentTargetPointNumber];

        transform.position = Vector3.MoveTowards(transform.position,
            _targetPoint, _speed * Time.deltaTime);

        if (transform.position == _targetPoint)
        {
            _currentTargetPointNumber++;

            if (_currentTargetPointNumber >= _points.Count)
            {
                _currentTargetPointNumber = 0;
                _points.Reverse();
            }
        }
    }

    private void InstantiatePointsPrefabs()
    {
        if (_pointPrefab)
            foreach (Vector3 currentPoint in _points)
                Instantiate(_pointPrefab, currentPoint, Quaternion.identity);
    }
}
