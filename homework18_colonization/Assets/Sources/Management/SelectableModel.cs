using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RTS.Management
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(MeshFilter))]
    public class SelectableModel : MonoBehaviour, ISelectable
    {
        [SerializeField, Required] private SelectableObject _selectableObject;

        public void Initialize()
        {
            ModelMeshFilter = GetComponent<MeshFilter>();
            ModelCollider = GetComponent<BoxCollider>();
        }

        public MeshFilter ModelMeshFilter { get; private set; }

        public BoxCollider ModelCollider { get; private set; }

        public void Select()
        {
            _selectableObject.Select();
        }

        public void Unselect()
        {
            _selectableObject.Unselect();
        }
    }
}
