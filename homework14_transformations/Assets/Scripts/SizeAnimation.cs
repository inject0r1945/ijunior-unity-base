using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAnimation : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField, Range(1, 5)] private float _scaleMultiplier = 3f;

    private Vector3 _startScale;
    private Vector3 _targetScale;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    private void Start()
    {
        SetTargetScale();
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetScale, Time.deltaTime * _speed);

        if (transform.localScale == _targetScale)
        {
            SetTargetScale();
        }
    }

    private void SetTargetScale()
    {
        if (transform.localScale == _startScale)
        {
            _targetScale = _startScale * _scaleMultiplier;
        }
        else
        {
            _targetScale = _startScale;
        }
    }
}
