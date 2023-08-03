using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Loot
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player playerCollision = collision.transform.GetComponent<Player>();

        if (playerCollision)
        {
            Destroy(transform.gameObject);
            _took.Invoke();
        }
    }
}
