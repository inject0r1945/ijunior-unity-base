using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rebound : MonoBehaviour
{
    [SerializeField] private float _force;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MakeRebound()
    {
        _velocity = _rigidbody.velocity;
        _velocity.y = 0;
        _rigidbody.velocity = _velocity;
        _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
    }
}
