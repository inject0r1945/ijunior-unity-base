using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS.Input
{
    public class PlayerInputData : InputData, IDisposable
    {
        private PlayerInputActions _playerInputActions = new();
        private InputAction _clickInputAction;
        private InputAction _clickPositionInputAction;

        public PlayerInputData()
        {
            Enable();

            _clickInputAction = _playerInputActions.LevelControl.Click;
            _clickPositionInputAction = _playerInputActions.LevelControl.ClickPosition;
            _clickInputAction.performed += OnPlayerClick;
        }

        public void Dispose()
        {
            _clickInputAction.performed -= OnPlayerClick;
            Disable();
        }

        public override void Enable()
        {
            _playerInputActions.Enable();
        }

        public override void Disable()
        {
            _playerInputActions.Disable();
        }

        public override Vector2 ReadScreenPosition()
        {
            return _clickPositionInputAction.ReadValue<Vector2>();
        }

        private void OnPlayerClick(InputAction.CallbackContext context)
        {
            SendClickEvent(ReadScreenPosition());
        }
    }
}
