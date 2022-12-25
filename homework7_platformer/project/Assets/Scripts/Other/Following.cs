using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0f, 100f)] private float _speed = 10f;
    [SerializeField] private float _verticalShift = 0f;

    private Vector3 _targetPoint;

    private void LateUpdate()
    {
        _targetPoint = _target.position;
        _targetPoint.y += _verticalShift;

        Vector3 destination = Vector3.MoveTowards(transform.position, _targetPoint, _speed * Time.deltaTime);
        destination.z = transform.position.z;
        transform.position = destination;
    }
}
