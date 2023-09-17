using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    private PlayerMover _mover;
    private int _score;
    private Health _health;

    public event UnityAction GameOver;

    public event UnityAction<int> ScoreChanged;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        Enemy.Died -= OnEnemyDied;
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _mover.Reset();
        _health.Reset();
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }

    private void OnDied(Health health)
    {
        GameOver?.Invoke();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        IncreaseScore();
    }
}
