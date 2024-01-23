using UnityEngine;

namespace Platformer.Core
{
    public class PatrolPointsInitializer : PointsInitializer
    {
        private void OnDrawGizmos()
        {
            if (Points == null)
                return;

            Gizmos.color = Color.green;

            for (int x = 0; x < Points.Count - 1; x++)
                Gizmos.DrawLine(Points[x].position, Points[x + 1].position);
        }
    }
}
