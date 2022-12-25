using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallConstraint : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _maxFallSpeed = 10f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < -_maxFallSpeed)
        {
            Vector2 constraintedVelocity = new Vector2();

            constraintedVelocity.x = _rigidbody.velocity.x;
            constraintedVelocity.y = -_maxFallSpeed;

            _rigidbody.velocity = constraintedVelocity;
        }  
    }
}
 