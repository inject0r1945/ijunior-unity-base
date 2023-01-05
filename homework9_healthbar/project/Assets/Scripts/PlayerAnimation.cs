using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private readonly string _hitTriggerName = "Hit";
    private readonly string _healTriggerName = "Heal";

    private int _hitTriggerHash;
    private int _healTriggerHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _hitTriggerHash = Animator.StringToHash(_hitTriggerName);
        _healTriggerHash = Animator.StringToHash(_healTriggerName);
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(_hitTriggerHash);
    }

    public void PlayHealAnimation()
    {
        _animator.SetTrigger(_healTriggerHash);
    }
}
