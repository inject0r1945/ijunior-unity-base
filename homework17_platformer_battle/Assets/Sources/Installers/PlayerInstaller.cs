using Platformer.Capabilities;
using Platformer.Playing;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Platformer.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField, Required, AssetsOnly] private Player _playerPrefab;
        [SerializeField, Required, SceneObjectsOnly] private Transform _spawnPoint;

        public override void InstallBindings()
        {
            Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);
            Container.Bind<Vampirism>().FromComponentOn(player.gameObject).AsSingle();
        }
    }
}