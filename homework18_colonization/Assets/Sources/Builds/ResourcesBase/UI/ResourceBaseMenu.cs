using RTS.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild.UI
{
    public class ResourceBaseMenu : UIScreen
    {
        [SerializeField, Required] private UIButton _createUnitButton;
        [SerializeField, Required] private UIButton _putFlagButton;
        [SerializeField, Required] private TMP_Text _detailsCountText;

        private ResourcesBaseMediator _resourcesBaseMediator;

        public void Initialize(ResourcesBaseMediator resourcesBaseMediator)
        {
            _resourcesBaseMediator = resourcesBaseMediator;
        }

        private void OnEnable()
        {
            _createUnitButton.Clicked += OnCreateUnitClick;
            _putFlagButton.Clicked += OnPutFlagClick;
        }

        private void OnDisable()
        {
            _createUnitButton.Clicked -= OnCreateUnitClick;
            _putFlagButton.Clicked -= OnPutFlagClick;
        }

        public void SetDetailsCount(int detailsCount)
        {
            _detailsCountText.text = detailsCount.ToString();
        }

        public void EnableCreateResourceCollectorButton()
        {
            _createUnitButton.Enable();
        }

        public void DisableCreateResourceCollectorButton()
        {
            _createUnitButton.Disable();
        }

        private void OnCreateUnitClick()
        {
            _resourcesBaseMediator.CreateResourceCollector();
        }

        private void OnPutFlagClick()
        {
            _resourcesBaseMediator.StartPutFlagBehaviour();
        }
    }
}