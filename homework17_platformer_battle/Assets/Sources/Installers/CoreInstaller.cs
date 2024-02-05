using Platformer.Core;
using Zenject;

namespace Platformer.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Disposabler>().AsSingle();
        }
    }
}
