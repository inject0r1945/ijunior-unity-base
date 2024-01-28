using MonoUtils;
using Platformer.Core;
using UnityEngine;

namespace Platformer.Playing
{
    public class Player : InitializedMonobehaviour, ITransform
    {
        private Transform _transform;

        public Transform Transform => _transform;

        public void Initialize()
        {
            _transform = transform;

            IsInitialized = true;
        }
    }
}