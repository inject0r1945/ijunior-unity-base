using HealthVisualization.Integer;
using Platformer.Attributes;
using Platformer.Capabilities;
using Platformer.Core;
using Platformer.Effects;
using Platformer.Playing;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Platformer.Bootstraps
{
    public class PlayerComposite : MonoBehaviour
    {
        [SerializeField, Required] private Player _player;
        [SerializeField, Required] private PlayerView _playerView;
        [SerializeField, Required] private Rigidbody2D _playerRigidbody;
        [SerializeField, Required] private Health _playerHealth;
        [SerializeField, Required] private FallSpeedLimiter _playerFallSpeedLimiter;
        [SerializeField, Required] private Blink _playerBlink;
        [SerializeField, Required] private PlayerStateMachine _playerStateMachine;
        [SerializeField, Required] private SpriteFlipper _playerSpriteFlipper;
        [SerializeField, Required] private Rebound _playerRebound;
        [SerializeField, Required] private HealthEventer _playerHealthEventer;
        [SerializeField, Required] private IntSmoothHealthBar _healthBar;

        [Inject]
        private void Construct(Disposabler disposabler)
        {
            Initialize(disposabler);
        }

        public void Initialize(Disposabler disposabler)
        {
            _player.Initialize();
            _playerHealth.Initialize();
            _playerHealthEventer.Initialize();
            _playerView.Initialize();
            _playerSpriteFlipper.Initialize(new RigidbodyVelocity(_playerRigidbody));
            _playerBlink.Initialize();
            _playerRebound.Initialize();
            _playerFallSpeedLimiter.Initialize();

            IntHealthMediator healthMediator = new(_playerHealth);
            disposabler.Add(healthMediator);

            _playerStateMachine.Initialize();
            _healthBar.Initialize(healthMediator);
        }
    }
}
