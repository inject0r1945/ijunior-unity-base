using UnityEngine;

namespace Platformer.Core
{
    public class Follower
    {
        private IAIMovable _agent;
        private Transform _target;

        public Follower(IAIMovable agent)
        {
            _agent = agent;
        }

        public bool HasTarget => _target != null;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Reset()
        {
            _target = null;
            _agent.Pathfinder.SetPath(null);
        }

        public void Update()
        {
            if (_target == null)
                return;

            _agent.Pathfinder.destination = _target.position;
            _agent.Pathfinder.maxSpeed = _agent.Speed;
        }
    }
}