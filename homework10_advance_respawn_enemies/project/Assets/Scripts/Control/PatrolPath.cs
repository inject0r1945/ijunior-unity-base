using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float WaypointGizmosRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), WaypointGizmosRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextWaypointIndex(i)));
            }
        }

        public Vector3 GetWaypoint(int index)
        {
            return transform.GetChild(index).position;
        }

        public int GetNextWaypointIndex(int currentIndex)
        {
            if (currentIndex >= transform.childCount - 1)
                return 0;

            return currentIndex + 1;
        }
    }
}
