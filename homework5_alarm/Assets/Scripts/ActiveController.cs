using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveController : MonoBehaviour
{
    private UnityEvent _enabled = new UnityEvent();
    private UnityEvent _disabled = new UnityEvent();

    public event UnityAction Enabled
    {
        add { _enabled.AddListener(value); }
        remove { _enabled.RemoveListener(value); }
    }

    public event UnityAction Disabled
    {
        add { _disabled.AddListener(value); }
        remove { _disabled.RemoveListener(value); }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _enabled.Invoke();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _disabled.Invoke();
    }
}
