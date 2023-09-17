using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorAnimation : MonoBehaviour
{
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _time = 1f;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _renderer.material.DOColor(_targetColor, _time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
