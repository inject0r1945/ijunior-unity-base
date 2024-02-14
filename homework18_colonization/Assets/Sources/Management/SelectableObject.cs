using Sirenix.OdinInspector;
using UnityEngine;

namespace RTS.Management
{
    public class SelectableObject : MonoBehaviour, ISelectable
    {
        [Title("Selection Settings")]
        [SerializeField] private GameObject _selectionIndicator;
        [SerializeField, Required, ChildGameObjectsOnly] private SelectableModel _selectableModel;

        public SelectableModel SelectableModel => _selectableModel;

        protected virtual void Start()
        {
            Unselect();
        }

        public virtual void Unselect()
        {
            _selectionIndicator.SetActive(false);
        }

        public virtual void Select()
        {
            _selectionIndicator.SetActive(true);
        }
    }
}
