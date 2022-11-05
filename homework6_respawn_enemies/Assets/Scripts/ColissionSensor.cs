using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class ColissionSensor : MonoBehaviour
{
    private CircleCollider2D _sensorCollider;
    private int _currentColissionCount;
    private UnityEvent _collissionExist = new UnityEvent();

    public bool IsColissionExist => _currentColissionCount > 0;

    public event UnityAction CollissionExist
    {
        add { _collissionExist.AddListener(value); }
        remove { _collissionExist.RemoveListener(value); }
    }

    private void Awake()
    {
        _sensorCollider = GetComponent<CircleCollider2D>();
        _sensorCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentColissionCount++;
        _collissionExist.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentColissionCount--;
    }
}
