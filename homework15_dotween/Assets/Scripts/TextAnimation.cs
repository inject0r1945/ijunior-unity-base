using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private string _targetText;
    [SerializeField] private ScrambleMode _scrambleMode;
    [SerializeField] private string _scrambleChars;
    [SerializeField] private float _time = 2f;

    private void Start()
    {
        _textField.DOText(_targetText, _time, scrambleMode: _scrambleMode, scrambleChars: _scrambleChars).SetLoops(-1);
    }
}
