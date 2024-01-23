using Platformer.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Bootstraps
{
    public class LevelComposite : Composite
    {
        [SerializeField, Required] private RandomPointsSpawner _healKitsSpawner;

        public override void Initialize()
        {
            _healKitsSpawner.Initialize();
        }
    }
}
