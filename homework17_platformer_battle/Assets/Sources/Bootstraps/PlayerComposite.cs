using HealthVisualization.Integer;
using Platformer.Attributes;
using Platformer.Capabilities;
using Platformer.Control;
using Platformer.Core;
using Platformer.Effects;
using Platformer.Playing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platformer.Bootstraps
{
    public class PlayerComposite : Composite
    {
        [SerializeField, Required] private Player _player;
        [SerializeField, Required] private PlayerView _playerView;
        [SerializeField, Required] private Rigidbody2D _playerRigidbody;
        [SerializeField, Required] private Mover _playerMover;
        [SerializeField, Required] private Jumper _playerJumper;
        [SerializeField, Required] private Health _playerHealth;
        [SerializeField, Required] private FallSpeedLimiter _playerFallSpeedLimiter;
        [SerializeField, Required] private Blink _playerBlink;
        [SerializeField, Required] private PlayerStateMachine _playerStateMachine;
        [SerializeField, Required] private SpriteFlipper _playerSpriteFlipper;
        [SerializeField, Required] private Rebound _playerRebound;
        [SerializeField, Required] private HealthEventer _playerHealthEventer;
        [SerializeField, Required] private IntSmoothHealthBar _healthBar;


        public override void Initialize()
        {
            _playerHealth.Initialize();
            _playerHealthEventer.Initialize();
            _playerView.Initialize();
            _playerSpriteFlipper.Initialize(new RigidbodyVelocity(_playerRigidbody));
            _playerBlink.Initialize();
            _playerRebound.Initialize();
            _playerFallSpeedLimiter.Initialize();
            _player.Initialize();

            InputEventer playerInputEventer = new PlayerInputEventer();
            _playerMover.Initialize(playerInputEventer);
            _playerJumper.Initialize(playerInputEventer);

            _playerStateMachine.Initialize();

            IntHealthMediator healthMediator = new(_playerHealth);
            _healthBar.Initialize(healthMediator);
        }
    }
}
