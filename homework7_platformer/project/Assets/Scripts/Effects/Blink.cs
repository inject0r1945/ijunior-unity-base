using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float _time = 1;
    [SerializeField] private float _speed = 30;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void StartEffect()
    {
        StartCoroutine(nameof(ShowBlink));
    }

    private IEnumerator ShowBlink()
    {
        float sinusShift = 0.5f;

        for (float t = 0; t < _time; t += Time.deltaTime)
        {
            float alphaChannelValue = Mathf.Sin(t * _speed) * sinusShift + sinusShift;

            Color newColor = Color.white;
            newColor.a = alphaChannelValue;

            _renderer.material.color = newColor;
            yield return null;
        }

        _renderer.material.color = _defaultColor;
    }
}
