using MonoUtils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rebound : InitializedMonobehaviour
    {
        [SerializeField, Required, MinValue(0)] private float _force;

        private Rigidbody2D _rigidbody;
        private Vector2 _velocity;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            IsInitialized = true;
        }

        public void Make()
        {
            _velocity = _rigidbody.velocity;
            _velocity.y = 0;
            _rigidbody.velocity = _velocity;
            _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }
    }
}
