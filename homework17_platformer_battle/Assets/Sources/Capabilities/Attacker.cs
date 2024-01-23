using Platformer.Attributes;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Capabilities
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField, Required, MinValue(0)] private int _damage = 1;

        private const float UpNormalThreshold = 1;
        private IDamageable[] _damageables;

        public UnityEvent Attacked;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ContactPoint2D contactPoint = collision.contacts.First();

            if (contactPoint.normal.y < UpNormalThreshold)
                return;

            _damageables = collision.gameObject.GetComponents<IDamageable>();

            if (_damageables.Length > 0)
            {
                foreach (IDamageable damageable in _damageables)
                    damageable.TakeDamage(_damage);

                Attacked?.Invoke();
            }
        }
    }
}
