using UnityEngine;

namespace Platformer.Bootstraps
{
    public class EnemyManagerComposite : Composite
    {
        private EnemyComposite[] _enemiesComposites;

        public override void Initialize()
        {
            _enemiesComposites = GetComponentsInChildren<EnemyComposite>();

            foreach (EnemyComposite enemyComposite in _enemiesComposites)
            {
                enemyComposite.Initialize();
            }
        }
    }
}
