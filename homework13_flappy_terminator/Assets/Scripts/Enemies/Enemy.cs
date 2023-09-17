using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ProjectileConfiguration _projectileConfiguration;
    [SerializeField] private Vector2 _projectileMinMaxDelay = new Vector2(0.5f, 5);
    [SerializeField] private LayerMask _projectileDamageMask = ~0;

    private Coroutine _spawnProjectileCoroutine;
    private Health _health;

    public static event UnityAction<Enemy> Died;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        if (_spawnProjectileCoroutine != null)
            StopCoroutine(_spawnProjectileCoroutine);

        _spawnProjectileCoroutine = StartCoroutine(SpawnProjectiles());

        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        if (_spawnProjectileCoroutine != null)
            StopCoroutine(_spawnProjectileCoroutine);

        _health.Died -= OnDied;
    }

    private IEnumerator SpawnProjectiles()
    {
        bool isEnd = false;

        while (!isEnd)
        {
            yield return new WaitForSeconds(Random.Range(_projectileMinMaxDelay.x, _projectileMinMaxDelay.y));

            _projectileConfiguration.LaunchProjectile(transform.position, transform.rotation, _projectileDamageMask);
        }
    }

    private void OnDied(Health health)
    {
        Died?.Invoke(this);
    }
}
