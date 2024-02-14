using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RTS.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField, Required] private Button _button;
        [SerializeField, Required] private Image _disableImage;

        public event UnityAction Clicked
        {
            add => _button.onClick.AddListener(value);
            remove =>_button.onClick.RemoveListener(value);
        }

        public void Enable()
        {
            _button.enabled = true;
            _disableImage.gameObject.SetActive(false);
        }

        public void Disable()
        {
            _button.enabled = false;
            _disableImage.gameObject.SetActive(true);
        }
    }
}
