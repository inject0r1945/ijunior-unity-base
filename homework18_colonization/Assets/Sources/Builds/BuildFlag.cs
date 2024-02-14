using RTS.Core;
using System;
using UnityEngine;

namespace RTS.Builds
{
    public class BuildFlag : MonoBehaviour, IMovable
    {
        private Transform _transform;

        public event Action<BuildFlagState> StateChanged;

        public BuildFlagState State { get; private set; }

        public Vector3 Position => _transform.position;

        public void Initialize()
        {
            _transform = transform;
            Free();
        }

        public void Move(Vector3 position)
        {
            _transform.position = position;
        }

        public void Free()
        {
            State = BuildFlagState.Free;
            Disable();

            StateChanged?.Invoke(State);
        }

        public void Positioning()
        {
            Enable();
            State = BuildFlagState.Positioning;

            StateChanged?.Invoke(State);
        }

        public void Put(Vector3 position)
        {
            Move(position);
            State = BuildFlagState.WaitBuild;

            StateChanged?.Invoke(State);
        }

        public void Complete()
        {
            if (State != BuildFlagState.WaitBuild)
                throw new Exception("Flag is not in wait build mode");

            State = BuildFlagState.Complete;
            Disable();

            StateChanged?.Invoke(State);
        }

        private void Enable()
        {
            gameObject.SetActive(true);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
