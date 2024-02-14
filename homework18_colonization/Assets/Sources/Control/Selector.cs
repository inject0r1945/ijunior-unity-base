using RTS.Input;
using RTS.Management;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RTS.Control
{
    public class Selector : IDisposable
    {
        private SelectableModel _selectedModel;
        private IInputData _inputData;
        private Camera _camera;

        public Selector(IInputData imputData)
        {
            _inputData = imputData;
            _camera = Camera.main;

            _inputData.Clicked += OnClick;
        }

        public void Dispose()
        {
            _inputData.Clicked -= OnClick;
        }

        private void OnClick(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out SelectableModel selectedModel))
            {
                if (_selectedModel != null && _selectedModel != selectedModel)
                    _selectedModel.Unselect();

                selectedModel.Select();
                _selectedModel = selectedModel;
            }
            else if (_selectedModel != null && EventSystem.current.IsPointerOverGameObject() == false)
            {
                _selectedModel.Unselect();
            }
        }
    }
}
