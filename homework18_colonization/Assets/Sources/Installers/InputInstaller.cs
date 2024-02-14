using RTS.Control;
using RTS.Input;
using System;
using Zenject;

namespace RTS.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.Bind(typeof(IDisposable), typeof(IInputData)).To<PlayerInputData>().AsSingle();
            Container.BindInterfacesAndSelfTo<Selector>().AsSingle();
        }
    }
}