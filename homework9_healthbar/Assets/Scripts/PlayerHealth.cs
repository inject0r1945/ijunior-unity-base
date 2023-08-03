using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _health = 100;
    
    private int _maxHealth;

    public event UnityAction<int, int> HealthChanged;


    private void Start()
    {
        _maxHealth = _health;
        HealthChanged.Invoke(_health, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        AddHealthDelta(-damage);
    }

    public void Heal(int healingSize)
    {
        AddHealthDelta(healingSize);
    }

    private void AddHealthDelta(int healthDelta)
    {
        _health += healthDelta;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        HealthChanged.Invoke(_health, _maxHealth);
    }
}
