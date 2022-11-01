using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveController : MonoBehaviour
{
    private UnityEvent _isEnable = new UnityEvent();
    private UnityEvent _isDisable = new UnityEvent();

    public event UnityAction IsEnable
    {
        add { _isEnable.AddListener(value); }
        remove { _isEnable.RemoveListener(value); }
    }

    public event UnityAction IsDisable
    {
        add { _isDisable.AddListener(value); }
        remove { _isDisable.RemoveListener(value); }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _isEnable.Invoke();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _isDisable.Invoke();
    }
}
