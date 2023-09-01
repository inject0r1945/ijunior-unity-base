using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _playerController.TryMoveUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _playerController.TryMoveDown();
        }
    }
}
