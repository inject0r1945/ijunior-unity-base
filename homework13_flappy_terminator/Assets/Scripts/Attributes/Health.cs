using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField, Range(0, 20)] private int _maxValue = 1;

    private int _currentValue;

    public event UnityAction<Health> Died;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new System.IndexOutOfRangeException(nameof(damage));

        _currentValue -= _currentValue;

        if (_currentValue <= 0)
            Died?.Invoke(this);
    }

    public void Die()
    {
        _currentValue = 0;
        Died?.Invoke(this);
    }
}
