using UnityEngine;

namespace Platformer.Core
{
    public class RigidbodyVelocity : IVelocity
    {
        private Rigidbody2D _rigidbody;

        public RigidbodyVelocity(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public Vector2 Velocity => _rigidbody.velocity;
    }
}
