using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage")]
public class Damage : ScriptableObject
{
    [SerializeField] private int _damageValue;

    public int DamageValue => _damageValue;
}
