using UnityEngine;

namespace Platformer.Capabilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rebound : MonoBehaviour
    {
        [SerializeField] private float _force;

        private Rigidbody2D _rigidbody;
        private bool _isInitialized;
        private Vector2 _velocity;

        private void Awake()
        {
            ValidateInitialization();
        }

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _isInitialized = true;
        }

        public void Make()
        {
            _velocity = _rigidbody.velocity;
            _velocity.y = 0;
            _rigidbody.velocity = _velocity;
            _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(Rebound)} is not initialized");
        }
    }
}
