using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _animationSpeed = 0.25f;
    [SerializeField] private PlayerHealth _playerHealth;

    private HealthLevel _healthLevel;
    private Image _healthLevelImage;
    private IEnumerator _healthLevelAnimationEnumerator;

    private void Awake()
    {
        _healthLevel = GetComponentInChildren<HealthLevel>();

        if (_healthLevel == null)
            throw new System.Exception("Не удалось найти компонент HealthLevel в дочерних элементах Healthbar");

        _healthLevelImage = _healthLevel.GetComponent<Image>();

        if (_healthLevelImage == null)
            throw new System.Exception("Не удалось найти компонент Image у объекта HealthLevel");
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += SetHealthLevel;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= SetHealthLevel;
    }

    public void SetHealthLevel(int health, int maxHealth)
    {
        if (_healthLevelAnimationEnumerator != null)
            StopCoroutine(_healthLevelAnimationEnumerator);

        float normalizedHealth = (float)health / maxHealth;

        _healthLevelAnimationEnumerator = StartHealthChangeAnimation(normalizedHealth);
        StartCoroutine(_healthLevelAnimationEnumerator);
    }

    private IEnumerator StartHealthChangeAnimation(float targetNormalizedHealthLevel)
    {
        while (_healthLevelImage.fillAmount != targetNormalizedHealthLevel)
        {
            _healthLevelImage.fillAmount = Mathf.MoveTowards(_healthLevelImage.fillAmount, 
                targetNormalizedHealthLevel, _animationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
