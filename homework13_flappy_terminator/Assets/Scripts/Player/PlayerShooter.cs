using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private ProjectileConfiguration _projectileConfiguration;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private KeyCode _shootButton;
    [SerializeField] private float _minDelay = 1f;
    [SerializeField] private LayerMask _projectileDamageMask = ~0;

    private float _shootTimer;

    private void Update()
    {
        if (Input.GetKeyDown(_shootButton) && _shootTimer > _minDelay)
            Shoot();

        UpdateTimers();
    }

    private void Shoot()
    {
        _projectileConfiguration.LaunchProjectile(_shootPoint.position, transform.rotation, _projectileDamageMask);
        _shootTimer = 0;
    }

    private void UpdateTimers()
    {
        _shootTimer += Time.deltaTime;
    }
}

