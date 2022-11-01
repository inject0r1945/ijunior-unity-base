using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _isEntered;

    private bool _isPlayerEntered;

    public bool IsPlayerEntered => _isPlayerEntered;

    public event UnityAction IsEntered
    {
        add { _isEntered.AddListener(value); }
        remove { _isEntered.RemoveListener(value); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isEntered.Invoke();

        if (collision.TryGetComponent<Player>(out Player player))
            _isPlayerEntered = true;
        else
            _isPlayerEntered = false;
    }
}
