using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _entered;

    private bool _isPlayerEntered;

    public bool IsPlayerEntered => _isPlayerEntered;

    public event UnityAction Entered
    {
        add { _entered.AddListener(value); }
        remove { _entered.RemoveListener(value); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _entered.Invoke();

        if (collision.TryGetComponent<Player>(out Player player))
            _isPlayerEntered = true;
        else
            _isPlayerEntered = false;
    }
}
