using Platformer.UI;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace Platformer.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField, Required] private VampirismView _vampirismView;

        public override void InstallBindings()
        {
            Container.Bind(typeof(VampirismMediator), typeof(IDisposable))
                .To<VampirismMediator>()
                .AsSingle()
                .WithArguments(_vampirismView)
                .NonLazy();
        }
    }
}
