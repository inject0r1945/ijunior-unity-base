using System;

namespace Platformer.Control
{
    public abstract class InputEventer
    {
        public event Action Jumped;

        public event Action JumpEnded;

        public event Action<float> Moved;

        protected void SendJumpEvent()
        {
            Jumped?.Invoke();
        }

        protected void SendJumpEndEvent()
        {
            JumpEnded?.Invoke();
        }

        protected void SendMoveEvent(float moveValue)
        {
            Moved?.Invoke(moveValue);
        }
    }
}
