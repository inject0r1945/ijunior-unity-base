using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthImagePrefab;

    private List<Image> _healthIcons = new List<Image>();

    public void Init(PlayerHealth playerHealth)
    {
        for (int i = 0; i < playerHealth.Health; i++)
        {
            Image newHealthImage = Instantiate(_healthImagePrefab, transform);
            _healthIcons.Add(newHealthImage);
        }
    }

    public void DisplayHealths(PlayerHealth playerHealth)
    {
        for (int healthIconIndex = 0; healthIconIndex < _healthIcons.Count; healthIconIndex++)
        {
            Image currentHealthIcon = _healthIcons[healthIconIndex];

            bool IsActiveHealthIcon = healthIconIndex < playerHealth.Health;

            currentHealthIcon.gameObject.SetActive(IsActiveHealthIcon);
        }
    }
}
