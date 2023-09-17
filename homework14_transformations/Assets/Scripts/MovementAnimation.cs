using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private bool _isYoYoMove = true;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        SetTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);

        if (transform.position == _targetPosition)
        {
            if (_isYoYoMove)
                SetTargetPosition();
            else
                transform.position = _startPosition;
        }
    }

    private void SetTargetPosition()
    {
        if (transform.position == _startPosition)
        {
            _targetPosition = _startPosition + transform.forward * _distance;
        }
        else
        {
            _targetPosition = _startPosition;
        }
    }
}
