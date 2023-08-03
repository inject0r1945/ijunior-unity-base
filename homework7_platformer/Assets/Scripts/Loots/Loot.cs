using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Loot : MonoBehaviour
{
    protected UnityEvent _took = new UnityEvent();

    public event UnityAction Took
    {
        add { _took.AddListener(value); }
        remove { _took.RemoveListener(value); }
    }
}
