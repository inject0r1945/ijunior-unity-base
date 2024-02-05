using System;
using System.Collections.Generic;

namespace Platformer.Core
{
    public class Disposabler : IDisposable
    {
        private List<IDisposable> _disposableObjects = new();

        public void Dispose()
        {
            foreach(IDisposable disposable in _disposableObjects)
            {
                disposable.Dispose();
            }
        }

        public void Add(IDisposable disposable)
        {
            _disposableObjects.Add(disposable);
        }

        public void Remove(IDisposable disposable)
        {
            _disposableObjects.Remove(disposable);
        }
    }
}
