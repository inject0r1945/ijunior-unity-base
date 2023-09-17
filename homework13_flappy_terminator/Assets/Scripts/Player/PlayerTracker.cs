using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private float _offsetX;

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x - _offsetX, transform.position.y, transform.position.z);
    }
}
