using Zenject;

namespace Platformer.Bootstraps
{
    public class EnemyManagerComposite : Composite
    {
        private EnemyComposite[] _enemiesComposites;

        [Inject]
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
