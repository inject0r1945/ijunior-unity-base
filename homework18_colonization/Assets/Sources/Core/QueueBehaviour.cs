using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTS.Core
{
    public class QueueBehaviour<T> : IClosestPuller<T> where T : MonoBehaviour
    {
        private List<T> _queue = new List<T>();
        private List<int> _memory = new List<int>();
        private bool _hasReAdding;

        public int Size => _queue.Count;

        public event Action QueueIncreased;

        public QueueBehaviour() : this(true) {}

        public QueueBehaviour (bool hasReAdding) 
        {
            _hasReAdding = hasReAdding;
        }

        public T PullClosest(Vector3 position)
        {
            T closestQueueElement = _queue.OrderBy(queueElement => Vector3.Distance(queueElement.transform.position, position)).FirstOrDefault();
            _queue.Remove(closestQueueElement);

            return closestQueueElement;
        }

        public void Push(List<T> elements)
        {
            foreach (T element in elements)
                Push(element);
        }

        public void Push(T element)
        {
            if (_queue.Contains(element))
                return;

            if (!_hasReAdding && _memory.Contains(element.GetInstanceID()))
                return;

            _queue.Add(element);

            if (!_hasReAdding)
                _memory.Add(element.GetInstanceID());

            QueueIncreased?.Invoke();
        }

        public T Pull()
        {
            T queueElement = _queue.FirstOrDefault();
            _queue.Remove(queueElement);

            return queueElement;
        }

        public void Remove(T element)
        {
            if (_queue.Contains(element))
                _queue.Remove(element);
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }
}
