using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementAnimation : MonoBehaviour
{
    [SerializeField] private float _time = 1f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private bool _isYoYoMove = true;

    private LoopType _loopType;

    private void Awake()
    {
        if (_isYoYoMove)
            _loopType = LoopType.Yoyo;
        else
            _loopType = LoopType.Restart;
    }

    private void Start()
    {
        transform.DOMoveZ(_distance, _time).SetLoops(-1, _loopType).SetEase(Ease.Linear);
    }
}
