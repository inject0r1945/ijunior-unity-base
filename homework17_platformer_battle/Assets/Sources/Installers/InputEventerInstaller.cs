using Platformer.Control;
using System;
using Zenject;

namespace Platformer.Installers
{
    public class InputEventerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(InputEventer), typeof(IDisposable)).To<PlayerInputEventer>().AsSingle();
        }
    }
}
