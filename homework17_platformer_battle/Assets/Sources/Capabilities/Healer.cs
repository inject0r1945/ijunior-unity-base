using Platformer.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Capabilities
{
    public class Healer : MonoBehaviour
    {
        [SerializeField, Required, MinValue(1)] private int _healSize = 1;
        [SerializeField] private LayerMask _targetLayers = ~0;

        [FoldoutGroup("Events")]
        public UnityEvent Healed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( (_targetLayers & (1 << collision.gameObject.layer)) == 0)
                return;

            if (collision.gameObject.TryGetComponent(out IHealing healing))
            {
                healing.Heal(_healSize);
                Healed?.Invoke();
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
