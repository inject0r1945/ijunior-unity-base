using UnityEngine;

namespace RTS.Core
{
    public interface IClosestPuller<T>
    {
        public T PullClosest(Vector3 position);
    }
}
