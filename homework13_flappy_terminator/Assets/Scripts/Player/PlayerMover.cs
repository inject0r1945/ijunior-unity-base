using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Vector3 _startPoition;
    [SerializeField] private float _tapForce = 5;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Rigidbody2D _rigibody;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigibody.velocity = Vector2.zero;
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        Reset();
    }

    private void Update()
    {
        Move();
    }

    public void Reset()
    {
        transform.position = _startPoition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _rigibody.velocity = Vector2.zero;
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigibody.velocity = new Vector2(0, 0);
            transform.rotation = _maxRotation;
            _rigibody.AddForce(Vector2.up * _tapForce, ForceMode2D.Impulse);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
}
