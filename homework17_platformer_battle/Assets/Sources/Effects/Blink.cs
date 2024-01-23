using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Platformer.Effects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Blink : MonoBehaviour
    {
        [SerializeField] private Color _blinkColor = Color.white;
        [SerializeField, Required, MinValue(0)] private float _time = 1;
        [SerializeField, Required, MinValue(0)] private float _speed = 30;

        private readonly float _sinusShift = 0.5f;
        private Renderer _renderer;
        private Color _defaultColor;
        private bool _isInitialized;

        public void Initialize()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _defaultColor = _renderer.material.color;

            _isInitialized = true;
        }

        public void StartBlink()
        {
            ValidateInitialization();
            StartCoroutine(nameof(StartBlinkCoroutine));
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(Blink)} is not initialized");
        }

        private IEnumerator StartBlinkCoroutine()
        {
            for (float time = 0; time < _time; time += Time.deltaTime)
            {
                float alphaChannelValue = Mathf.Sin(time * _speed) * _sinusShift + _sinusShift;

                _blinkColor.a = alphaChannelValue;
                _renderer.material.color = _blinkColor;

                yield return null;
            }

            _renderer.material.color = _defaultColor;
        }
    }
}