using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Checkpoint : MonoBehaviour
{
    private Animator _animator;
    private readonly string _reachedTriggerName = "IsReached";
    private int _reachedTriggerHash;
    private bool _isReached;
    private UnityEvent _reached = new UnityEvent();

    public bool IsReached => _isReached;

    public event UnityAction Reached
    {
        add { _reached.AddListener(value); }
        remove { _reached.RemoveListener(value); }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _reachedTriggerHash = Animator.StringToHash(_reachedTriggerName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Player>(out Player player))
        {
            _animator?.SetTrigger(_reachedTriggerHash);
            _isReached = true;

            _reached.Invoke();
        }
    }
}
