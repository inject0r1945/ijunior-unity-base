using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _mainSound;
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _healSound;

    private void Awake()
    {
        if (_mainSound)
            _mainSound.loop = true;

        if (_healSound)
            _healSound.loop = false;

        if (_hitSound)
            _hitSound.loop = false;
    }

    private void Start()
    {
        if (_mainSound)
         _mainSound.Play();
    }

    public void OnHit()
    {
        if (_hitSound)
            _hitSound.Play();
    }

    public void OnHeal()
    {
        if (_healSound)
            _healSound.Play();
    }
}
