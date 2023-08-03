using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] Movement _playerMovement;
    [SerializeField] Rigidbody2D _playerRigidbody;
    [SerializeField] private CheckpointsManager _checkpointsManager;

    [Header("Audios")]
    [SerializeField] private AudioSource _mainAudio;
    [SerializeField] private AudioSource _completeAudio;
    [SerializeField] private AudioSource _dieAudio;

    [Header("Canvases")]
    [SerializeField] private Canvas _finishCanvas;
    [SerializeField] private Canvas _dieCanvas;
    
    private bool _islevelCompleted;

    private void Start()
    {
        _mainAudio?.Play();
        _finishCanvas?.gameObject.SetActive(false);
        _dieCanvas?.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _checkpointsManager.LevelFinished += OnLevelComplete;
        _playerHealth.PlayerDied += OnLevelComplete;
    }

    private void OnDisable()
    {
        _checkpointsManager.LevelFinished -= OnLevelComplete;
        _playerHealth.PlayerDied -= OnLevelComplete;
    }

    private void FixedUpdate()
    {
        if (_islevelCompleted)
            _playerRigidbody.velocity = Vector2.zero;
    }

    private void OnLevelComplete()
    {
        _mainAudio?.Stop();
        _playerMovement.enabled = false;
        _playerRigidbody.isKinematic = true;

        if (_playerHealth.IsPlayerDied)
        {
            _dieCanvas?.gameObject.SetActive(true);
            _dieAudio?.Play();
        }
        else
        {
            _finishCanvas?.gameObject.SetActive(true);
            _completeAudio.Play();
        }

        _islevelCompleted = true;
    }
}
