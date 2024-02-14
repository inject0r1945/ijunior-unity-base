using System;
using UnityEngine;

namespace RTS.Input
{
    public interface IInputData
    {
        public event Action<Vector2> Clicked;

        public abstract Vector2 ReadScreenPosition();
    }
}
