using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Damage _targetDamage;

    public Damage TargetDamage => _targetDamage;
}
