using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float _time = 5f;

    private Vector3 _targetRotation = new Vector3(0, 360, 0);

    private void Start()
    {
        transform.rotation = Quaternion.identity;
        transform.DORotate(_targetRotation, _time, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
