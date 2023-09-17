using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfiguration", menuName = "Weapons/Projectile")]
public class ProjectileConfiguration : ScriptableObject
{
    [SerializeField] private Projectile _prefab;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 20f;

    public void LaunchProjectile(Vector3 position, Quaternion rotation, LayerMask layerMask)
    {
        Projectile projectile = Instantiate(_prefab, position, rotation);
        projectile.Init(_speed, _lifeTime, _damage, layerMask);
    }
}
