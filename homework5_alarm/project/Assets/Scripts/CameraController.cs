using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Update()
    {
        Vector3 target_position = _player.position;
        target_position.z = transform.position.z;
        float cameraYOffest = 5;
        target_position.y += cameraYOffest;

        transform.position = Vector3.Lerp(transform.position, target_position, Time.deltaTime);
    }
}
