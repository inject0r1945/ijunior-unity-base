using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Core
{
    public class Patroler
    {
        private IAIMovable _agent;
        private Queue<Transform> _points;
        private Transform _target;
        private bool _isStopped;

        public Patroler(IAIMovable agent, IEnumerable<Transform> points)
        {
            _agent = agent;
            _points = new Queue<Transform>(points);
        }

        public void Update()
        {
            if (_isStopped)
                return;

            if (_agent.Pathfinder.reachedEndOfPath)
            {
                ChangeTarget();
                UpdateAgent();
            }
        }

        public void Run()
        {
            UpdateAgent();
            _isStopped = false;
        }

        public void Stop()
        {
            _agent.Pathfinder.SetPath(null);
            _isStopped = true;
        }

        private void UpdateAgent()
        {
            if (_target == null)
                _target = _points.Dequeue();

            _agent.Pathfinder.SetPath(null);
            _agent.Pathfinder.destination = _target.position;
            _agent.Pathfinder.maxSpeed = _agent.Speed;
        }

        private void ChangeTarget()
        {
            if (_target != null)
                _points.Enqueue(_target);

            _target = _points.Dequeue();
        }
    }
}