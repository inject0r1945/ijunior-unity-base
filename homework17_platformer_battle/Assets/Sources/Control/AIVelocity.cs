using Pathfinding;
using Platformer.Core;
using UnityEngine;

namespace Platformer.Control
{
    public class AIVelocity : IVelocity
    {
        private AIPath _agent;

        public AIVelocity(AIPath agent)
        {
            _agent = agent;
        }

        public Vector2 Velocity => _agent.velocity;
    }
}