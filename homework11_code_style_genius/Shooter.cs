using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _pauseBetweenShots;
    [SerializeField] private GameObject _bulletPrefab;

    private void Start() 
    {
        StartCoroutine(nameof(Shoot));
    }

    private IEnumerator Shoot()
    {
        bool isEnabledComponent = enabled;
        var waitForSeconds = new WaitForSeconds(_pauseBetweenShots);
        Rigidbody bulletRigidbody;
        Vector3 shootingDirection;
        GameObject bullet;

        while (isEnabledComponent)
        {
            shootingDirection = (_target.position - transform.position).normalized;
            bullet = Instantiate(_bulletPrefab, transform.position + shootingDirection, Quaternion.identity);

            bulletRigidbody = bullet.GetComponent<Rigidbody>();

            bulletRigidbody.transform.up = shootingDirection;
            bulletRigidbody.velocity = shootingDirection * _bulletSpeed;

            yield return waitForSeconds;
         }
    }
}