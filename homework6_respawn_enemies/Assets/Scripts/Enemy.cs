using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _isRandomDirection = true;
    
    private ColissionSensor[] _wallSensors;
    private ColissionSensor _groundSensor;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private float _rotationMax = 20f;

    private int WalkDirection => (_spriteRenderer.flipX == true) ? 1 : -1;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        ColissionSensor wallSensorLeft = transform.Find("WallSensorLeft")?.GetComponent<ColissionSensor>();

        if (wallSensorLeft == null)
            throw new System.Exception("Не найден сенсор препятствий слева");

        ColissionSensor wallSensorRight = transform.Find("WallSensorRight")?.GetComponent<ColissionSensor>();

        if (wallSensorRight == null)
            throw new System.Exception("Не найден сенсор препятствий справа");

        _wallSensors = new ColissionSensor[] { wallSensorLeft, wallSensorRight };

        _groundSensor = transform.Find("GroundSensor").GetComponent<ColissionSensor>();

        if (_groundSensor == null)
            throw new System.Exception("Не найден сенсор земли");

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.flipX = true;
        int randomFlipThreshold = 1;
        int maxRandomValue = 2;

        if (_isRandomDirection)
            _spriteRenderer.flipX = (Random.Range(0, maxRandomValue) == randomFlipThreshold) ? true : false; 
    }

    private void OnEnable()
    {
        foreach (ColissionSensor wallSensor in _wallSensors)
            wallSensor.CollissionExist += OnWallDetect;
    }

    private void OnDisable()
    {
        foreach (ColissionSensor wallSensor in _wallSensors)
            wallSensor.CollissionExist -= OnWallDetect;
    }

    private void FixedUpdate()
    {
        _rigidBody.rotation = Mathf.Clamp(_rigidBody.rotation, _rotationMax * -1, _rotationMax);
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * WalkDirection, 0, 0);
        _animator.SetBool("IsGround", _groundSensor.IsColissionExist);
    }

    private void OnWallDetect()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
