using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;

    private int _health;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health);
    }

    public void ApplyDamage(Enemy enemy)
    {
        _health -= enemy.Damage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Die();
    }

    public void Heal(HealthKit healthKit)
    {
        _health += healthKit.HealthRecoveryAmount;

        if (_health > _maxHealth)
            _health = _maxHealth;

        HealthChanged?.Invoke(_health);
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
