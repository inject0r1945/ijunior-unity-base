using RTS.Management;
using RTS.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RTS.Builds
{
    public class Build : SelectableObject
    {
        [SerializeField, Required, ChildGameObjectsOnly] private UIScreen _buildMenu;

        public override void Select()
        {
            base.Select();
            _buildMenu.Enable();
        }

        public override void Unselect()
        {
            base.Unselect();
            _buildMenu.Disable();
        }
    }
}
