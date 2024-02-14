using RTS.AI;
using RTS.Core;
using Zenject;

namespace RTS.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMonoBehaviourContext();
            BindGridBaker();
        }

        public void BindMonoBehaviourContext()
        {
            Container.Bind<ICoroutine>().To<MonoBehaviourContext>().FromNewComponentOnRoot().AsSingle();
        }

        public void BindGridBaker()
        {
            Container.Bind<GridBaker>().AsSingle();
        }
    }
}
