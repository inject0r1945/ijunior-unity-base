using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Control
{
    public class PlayerInputReceiver : InputReceiver, IDisposable
    {
        private const float NoMoveInputValue = 0;

        private PlayerInputActions _inputActions = new();
        private InputAction _jumpAction;
        private InputAction _moveAction;

        public PlayerInputReceiver()
        {
            _jumpAction = _inputActions.Levels.Jump;
            _moveAction = _inputActions.Levels.Move;

            Enable();
        }

        public void Dispose()
        {
            Disable();
        }

        public void Enable()
        {
            _inputActions.Levels.Enable();
            Subscribe();
        }

        public void Disable()
        {
            Unsubscribe();
            _inputActions.Levels.Disable();
        }

        private void Subscribe()
        {
            _jumpAction.started += OnJumpStart;
            _jumpAction.canceled += OnJumpEnd;
            _jumpAction.performed += OnJumpEnd;
            _moveAction.started += OnMove;
            _moveAction.canceled += OnMoveCanceled;
            _moveAction.performed += OnMove;
        }

        private void OnJumpStart(InputAction.CallbackContext context)
        {
            SendJumpEvent();
        }

        private void OnJumpEnd(InputAction.CallbackContext context)
        {
            SendJumpEndEvent();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            SendMoveEvent(_moveAction.ReadValue<float>());
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            SendMoveEvent(NoMoveInputValue);
        }

        private void Unsubscribe()
        {
            _jumpAction.started -= OnJumpStart;
            _jumpAction.canceled -= OnJumpEnd;
            _jumpAction.performed -= OnJumpEnd;
            _moveAction.started -= OnMove;
            _moveAction.canceled -= OnMoveCanceled;
            _moveAction.performed -= OnMove;
        }
    }
}