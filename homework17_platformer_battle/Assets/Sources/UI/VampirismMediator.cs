using Platformer.Capabilities;
using System;

namespace Platformer.UI
{
    public class VampirismMediator : IDisposable
    {
        private VampirismView _view;
        private Vampirism _vampirismAbility;

        public VampirismMediator(VampirismView view, Vampirism vampirismAbility)
        {
            _view = view;
            _vampirismAbility = vampirismAbility;

            SetAvailableVampirismView();

            _vampirismAbility.Started += OnVampirismStart;
            _vampirismAbility.Blocked += OnVampirismBlocked;
            _vampirismAbility.Recovered += OnVampirismRecovered;
        }

        public void Dispose()
        {
            _vampirismAbility.Started -= OnVampirismStart;
            _vampirismAbility.Blocked -= OnVampirismBlocked;
            _vampirismAbility.Recovered -= OnVampirismRecovered;
        }

        private void SetAvailableVampirismView()
        {
            _view.SetAvailableView();
        }

        private void OnVampirismStart()
        {
            _view.SetActivatedView();
        }

        private void OnVampirismBlocked(float recoveryTime)
        {
            _view.SetLockViewBy(recoveryTime);
        }

        private void OnVampirismRecovered()
        {
            SetAvailableVampirismView();
        }
    }
}
