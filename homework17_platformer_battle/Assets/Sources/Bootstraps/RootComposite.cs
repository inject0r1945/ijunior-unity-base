using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Bootstraps
{
    public class RootComposite : MonoBehaviour
    {
        [SerializeField, Required, SceneObjectsOnly] private PlayerComposite _playerComposite;
        [SerializeField, Required, SceneObjectsOnly] private EnemyManagerComposite _enemyManagerComposite;
        [SerializeField, Required, SceneObjectsOnly] private LevelComposite _levelComposite;

        private void Awake()
        {
            _playerComposite.Initialize();
            _enemyManagerComposite.Initialize();
            _levelComposite.Initialize();
        }
    }
}
