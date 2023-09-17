using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _playerController.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _playerController.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}
