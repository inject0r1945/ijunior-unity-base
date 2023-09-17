using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _button.interactable = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _button.interactable = false;
    }

    protected abstract void OnButtonClick();
}
