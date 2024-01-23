using Platformer.Core;
using UnityEngine;

namespace Platformer.Playing
{
    public class Player : MonoBehaviour, ITransform
    {
        private Transform _transform;
        private bool _isInitialized;

        public Transform Transform => _transform;

        private void Awake()
        {
            ValidateInitialization();
        }

        private void FixedUpdate()
        {
            ValidateInitialization();
        }

        public void Initialize()
        {
            _transform = transform;
            _isInitialized = true;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(Player)} is not initialized");
        }
    }
}