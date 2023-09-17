using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private void Update()
    {
        Vector3 currentEulerRotaion = transform.rotation.eulerAngles;
        Quaternion nextRotation = Quaternion.Euler(new Vector3(currentEulerRotaion.x, currentEulerRotaion.y + _speed * Time.deltaTime, currentEulerRotaion.z));
        transform.rotation = nextRotation;
    }
}
