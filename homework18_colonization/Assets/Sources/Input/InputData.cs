using System;
using UnityEngine;

namespace RTS.Input
{
    public abstract class InputData : IInputData
    {
        public event Action<Vector2> Clicked;

        public abstract void Enable();

        public abstract void Disable();

        public abstract Vector2 ReadScreenPosition();

        protected void SendClickEvent(Vector2 screenPosition)
        {
            Clicked?.Invoke(screenPosition);
        }
    }
}
