using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Blink))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField, Range(0f, 10f)] private float _invulnerableTime = 1;
    [SerializeField] private HealthUI _healthUI;
    [SerializeField] private UnityEvent _tookDamage;

    private bool _invulnerable;
    private Blink _blink;
    private UnityEvent _healthChanged = new UnityEvent();
    private UnityEvent _playerDied = new UnityEvent();

    public int Health => _health;

    public bool IsPlayerDied => _health == 0;

    public event UnityAction TookDamage
    {
        add { _tookDamage.AddListener(value); }
        remove { _tookDamage.RemoveListener(value); }
    }

    public event UnityAction HealthChanged
    {
        add { _healthChanged.AddListener(value); }
        remove { _healthChanged.RemoveListener(value); }
    }

    public event UnityAction PlayerDied
    {
        add { _playerDied.AddListener(value); }
        remove { _playerDied.RemoveListener(value); }
    }

    private void Awake()
    {
        _blink = GetComponent<Blink>();
    }

    private void Start()
    {
        _healthUI.Init(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    private void ProcessCollision(Collision2D collision)
    {
        Enemy enemyColission = collision.transform.GetComponent<Enemy>();

        if (enemyColission)
        {
            Collider2D enemyCollider = collision.transform.GetComponent<Collider2D>();
            TakeDamage(enemyColission.TargetDamage, enemyCollider);
        }
    }

    private void TakeDamage(Damage damage, Collider2D enemyCollider)
    {
        if (_invulnerable || damage.DamageValue == 0)
            return;

        _health -= damage.DamageValue;

        if (_health < 0)
            _health = 0;

        _healthChanged.Invoke();
        _tookDamage.Invoke();

        if (_health == 0)
            _playerDied.Invoke();

        _healthUI.DisplayHealths(this);

        StartCoroutine(nameof(StartInvulnerableEffect));
        StartCoroutine(StartIgnoreEnemyColissionsEffect(enemyCollider));
        _blink.StartEffect();
    }

    private IEnumerator StartInvulnerableEffect()
    {
        _invulnerable = true;

        yield return new WaitForSeconds(_invulnerableTime);

        _invulnerable = false;
    }

    private IEnumerator StartIgnoreEnemyColissionsEffect(Collider2D enemyCollider)
    {
        enemyCollider.isTrigger = true;

        yield return new WaitForSeconds(_invulnerableTime);

        enemyCollider.isTrigger = false;
    }
}
