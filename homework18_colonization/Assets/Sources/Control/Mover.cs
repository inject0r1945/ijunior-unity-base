using UnityEngine;
using Pathfinding;

namespace RTS.Control
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        private AIPath _aiPath;

        public void Initialize()
        {
            _aiPath = GetComponent<AIPath>();
        }

        public void Move(Vector3 position)
        {
            _aiPath.canSearch = true;
            _aiPath.destination = position;
        }

        public void Stop()
        {
            _aiPath.canSearch = false;
            _aiPath.SetPath(null);
        }
    }
}
