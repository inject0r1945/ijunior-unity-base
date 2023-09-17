using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SizeAnimation : MonoBehaviour
{
    [SerializeField] private float _time = 2f;
    [SerializeField, Range(1, 5)] private float _scaleMaxSize = 3f;

    private void Start()
    {
        transform.DOScale(_scaleMaxSize, _time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
