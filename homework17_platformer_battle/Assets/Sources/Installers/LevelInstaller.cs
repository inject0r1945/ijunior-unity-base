using Platformer.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Platformer.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField, Required] private RandomPointsSpawner _healKitsSpawner;

        public override void InstallBindings()
        {
            Container.Bind<IInitializable>().FromInstance(_healKitsSpawner);
        }
    }
}
