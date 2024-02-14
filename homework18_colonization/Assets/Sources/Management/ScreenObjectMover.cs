using RTS.Core;
using RTS.Input;
using System;
using UnityEngine;
using Zenject;

namespace RTS.Management
{
    public class ScreenObjectMover : ITickable
    {
        private const int DefaultSurfacesToMoveMask = ~0;
        private IInputData _inputData;
        private LayerMask _surfacesToMoveMask;
        private Camera _camera;
        private IMovable _movableObject;
        private Vector3 _lastObjectPosition;

        public ScreenObjectMover(IInputData inputData)
        {
            _inputData = inputData;
            _camera = Camera.main;
        }

        public event Action<Vector3> Moved;

        public void Tick()
        {
            StartMoveBehaviour();
        }

        public void Move(IMovable movableObject)
        {
            Move(movableObject, DefaultSurfacesToMoveMask);
        }

        public void Move(IMovable movableObject, LayerMask surfacesToMoveMask)
        {
            _movableObject = movableObject;
            _surfacesToMoveMask = surfacesToMoveMask;
            _inputData.Clicked += OnClick;
        }

        private void StartMoveBehaviour()
        {
            if (_movableObject == null)
                return;

            if (TryGetNextObjectPosition(out Vector3 nextPosition))
            {
                _lastObjectPosition = nextPosition;
                _movableObject.Move(nextPosition);
            } 
        }

        private bool TryGetNextObjectPosition(out Vector3 position)
        {
            position = Vector3.zero;
            Vector2 screenPosition = _inputData.ReadScreenPosition();
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if ((_surfacesToMoveMask.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    position = hit.point;

                    return true;
                }
            }

            return false;
        }

        private void OnClick(Vector2 screenPosition)
        {
            _inputData.Clicked -= OnClick;
            _movableObject = null;

            Moved?.Invoke(_lastObjectPosition);
        }
    }
}
