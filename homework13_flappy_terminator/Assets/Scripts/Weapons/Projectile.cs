using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour, IDamageable
{
    private float _speed;
    private float _lifetime;
    private float _lifeTimer;
    private int _damage;
    private LayerMask _layerMask;

    public float Speed
    {
        get => _speed;

        private set
        {
            if (value < 0)
                value = 0;

            _speed = value;
        }
    }

    public int Damage
    {
        get => _damage;

        private set
        {
            if (value < 0)
                value = 0;

            _damage = value;
        }
    }

    private void Update()
    {
        Move();
        UpdateTimers();

        if (_lifeTimer >= _lifetime)
            Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int filteredLayers = _layerMask & (1 << collision.gameObject.layer);

        if (filteredLayers == 0)
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageableObject))
        {
            damageableObject.TakeDamage(Damage);
        }

        Die();
    }

    public void TakeDamage(int damage)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Init(float speed, float lifetime, int damage, LayerMask layerMask)
    {
        Init(speed, lifetime, damage);
        _layerMask = layerMask;
    }

    public void Init(float speed, float lifetime, int damage)
    {
        Speed = speed;
        Damage = damage;
        _lifetime = lifetime;
        _layerMask = ~0;
    }

    private void Move()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void UpdateTimers()
    {
        _lifeTimer += Time.deltaTime;
    }
}
