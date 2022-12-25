using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    private Image _damageImage;

    private void Awake()
    {
        _damageImage = GetComponent<Image>();
    }

    private void Start()
    {
        _damageImage.enabled = false;
    }

    public void StartEffect()
    {
        StartCoroutine(nameof(ShowDamageImage));
    }

    private IEnumerator ShowDamageImage()
    {
        _damageImage.enabled = true;
        Color damageImageColor = Color.red;

        for (float alphaPercent = 1f; alphaPercent > 0; alphaPercent -= Time.deltaTime)
        {
            damageImageColor.a = alphaPercent;
            _damageImage.color = damageImageColor;

            yield return null;
        }

        _damageImage.enabled = false;
    }
}
