using System.Collections;
using UnityEngine;

namespace RTS.Core
{
    public interface ICoroutine
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);

        public void StopCoroutine(Coroutine coroutine);
    }
}
