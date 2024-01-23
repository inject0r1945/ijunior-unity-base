using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer
{
    public class ParticlesCreator : MonoBehaviour
    {
        [SerializeField, Required, AssetsOnly] private ParticleSystem[] _particlePrefabs;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Create()
        {
            foreach (ParticleSystem particlePrefab in _particlePrefabs)
            {
                ParticleSystem particle = Instantiate(particlePrefab);

                particle.gameObject.transform.position = _transform.position;

                if (particle.isPlaying == false)
                    particle.Play();
            }
        }
    }
}
