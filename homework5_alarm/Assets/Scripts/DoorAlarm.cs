using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorAlarm : MonoBehaviour
{
    [SerializeField] private ActiveController _activeController;
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _maxVolume = 1.0f;
    [SerializeField] private float _speedVolumeChange = 0.7f;

    private float _minVolume = 0f;
    private DoorTrigger[] _doorTriggers;
    private Coroutine _changeAlarmVolumeCoroutine;
    private float _currentVolume;

    private void Awake()
    {
        _doorTriggers = GetComponentsInChildren<DoorTrigger>();
        _alarmSound = GetComponent<AudioSource>();
        _alarmSound.volume = 0;
        _alarmSound.loop = true;
        _maxVolume = Mathf.Clamp(_maxVolume, 0f, 1f);
    }

    private void OnEnable()
    {
        _activeController.Enabled += StopAlarm;
        _activeController.Disabled += StartAlarm;
    }

    private void OnDisable()
    {
        _activeController.Enabled -= StopAlarm;
        _activeController.Disabled -= StartAlarm;
    }

    private void StartAlarm()
    {
        bool isStopAction = false;
        ManupulateAlarm(isStopAction);
    }

    private void StopAlarm()
    {
        bool isStopAction = true;
        ManupulateAlarm(isStopAction);
    }

    private void ManupulateAlarm(bool isStopAction)
    {
        float _targetVolume = (isStopAction) ? _minVolume : _maxVolume;

        foreach (DoorTrigger doorTrigger in _doorTriggers)
        {
            bool isRequiredVolume = (isStopAction) ? _currentVolume < _minVolume : _currentVolume > _minVolume;

            if (doorTrigger.IsPlayerEntered && !isRequiredVolume)
            {
                if (_changeAlarmVolumeCoroutine is not null)
                    StopCoroutine(_changeAlarmVolumeCoroutine);

                _changeAlarmVolumeCoroutine = StartCoroutine(ChangeAlarmVolume(_targetVolume));

                break;
            }
        }
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        bool isStop = false;

        if (targetVolume > 0 && _alarmSound.isPlaying == false)
            _alarmSound.Play();

        while (isStop == false)
        {
            _currentVolume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, Time.deltaTime * _speedVolumeChange);
            _alarmSound.volume = _currentVolume;

            if (_alarmSound.volume == targetVolume)
            {
                isStop = true;

                if (_alarmSound.volume == 0 && _alarmSound.isPlaying)
                    _alarmSound.Stop();
            }  

            yield return null;
        } 
    }
}
