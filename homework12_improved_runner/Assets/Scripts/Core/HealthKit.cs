using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private int _healthRecoveryAmount = 1;

    public int HealthRecoveryAmount => _healthRecoveryAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Use(player);
        }
    }

    private void Use(Player player)
    {
        player.Heal(this);
        gameObject.SetActive(false);
    }
}
