using Sirenix.OdinInspector;
using UnityEngine;

namespace RTS.Units
{
    [CreateAssetMenu(fileName = "new UnitsConfigurations", menuName = "UnitsConfigurations")]
    public class UnitsConfigurations : ScriptableObject
    {
        [field: SerializeField, Required] public ResourcesCollectorUnit ResourcesCollectorUnitPrefab { get; private set; }
    }
}
