using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Shake : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField, Range(0, 5)] private float _shakeDuration = 0.3f;
    [SerializeField, Range(0, 3)] private float _strength = 0.2f;
    [SerializeField, Range(0, 1)] private float _randomness = 0f;

    private Tween _shakeTween;

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (_shakeTween == null || _shakeTween.active == false)
            _shakeTween = transform.DOShakeScale(_shakeDuration, 
                randomnessMode: ShakeRandomnessMode.Harmonic, strength: _strength,
                randomness: _randomness);
    }
}
