using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _pauseBetweenShots;
    [SerializeField] private Bullet _bulletPrefab;

    private void Start() 
    {
        StartCoroutine(nameof(Shoot));
    }

    private IEnumerator Shoot()
    {
        bool isEnabledComponent = enabled;
        Vector3 shootingDirection;
        Bullet bullet;
        Rigidbody bulletRigidbody;
        var waitForSeconds = new WaitForSeconds(_pauseBetweenShots);

        while (isEnabledComponent)
        {
            shootingDirection = (_target.position - transform.position).normalized;
            bullet = Instantiate(_bulletPrefab, transform.position + shootingDirection, Quaternion.identity);

            bulletRigidbody = bullet.GetComponent<Rigidbody>();

            bulletRigidbody.transform.up = shootingDirection;
            bulletRigidbody.velocity = shootingDirection * _bulletSpeed;
            
            isEnabledComponent = enabled;

            yield return waitForSeconds;
         }
    }
}