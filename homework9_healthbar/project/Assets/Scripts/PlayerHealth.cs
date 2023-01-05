using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _health = 100;
    [SerializeField, Range(0, 100)] private int _damageSize = 10;
    [SerializeField, Range(0, 100)] private int _healingSize = 10;
    [SerializeField] private Healthbar _healthbar;

    private int _maxHealth = 100;

    public float NormalizedHealth => (float)_health / _maxHealth;

    private void Start()
    {
        _healthbar.Init(NormalizedHealth);
    }

    public void TakeDamage()
    {
        AddHealthDelta(-_damageSize);
    }

    public void Heal()
    {
        AddHealthDelta(_healingSize);
    }

    private void AddHealthDelta(int healthDelta)
    {
        _health += healthDelta;

        if (_health > _maxHealth)
            _health = _maxHealth;
        else if (_health < 0)
            _health = 0;

        _healthbar.SetHealthLevel(NormalizedHealth);
    }
}
