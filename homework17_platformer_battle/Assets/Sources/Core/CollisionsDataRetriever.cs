using Platformer.Enemies;
using Platformer.Environment;
using UnityEngine;

namespace Platformer.Core
{
    public class CollisionsDataRetriever : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _normalThreshold = 0.9f;

        private bool _onGround;
        private bool _onWall;
        private float _friction;

        public bool OnGround => _onGround;

        public bool OnWall => _onWall;

        public float Friction => _friction;

        public Vector2 ContactNormal { get; private set; }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CalculateCollisionLocations(collision);
            CalculateCollisionFriction(collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CalculateCollisionLocations(collision);
            CalculateCollisionFriction(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _onGround = false;
            _onWall = false;
            _friction = 0;
        }

        public void CalculateCollisionLocations(Collision2D collision)
        {
            for (int collisionNumber = 0; collisionNumber < collision.contactCount; collisionNumber++)
            {
                ContactPoint2D currentColission = collision.GetContact(collisionNumber);
                ContactNormal = currentColission.normal;

                _onGround |= ContactNormal.y >= _normalThreshold;

                Enemy enemyColission = collision.transform.GetComponent<Enemy>();
                Platform platformCollision = collision.transform.GetComponent<Platform>();

                _onWall |= Mathf.Abs(ContactNormal.x) >= _normalThreshold && enemyColission == false && platformCollision == false;
            }
        }

        private void CalculateCollisionFriction(Collision2D collision)
        {
            PhysicsMaterial2D collisionMaterial = collision.collider.sharedMaterial;

            _friction = 0f;

            if (collisionMaterial)
                _friction = collisionMaterial.friction;
        }
    }
}
