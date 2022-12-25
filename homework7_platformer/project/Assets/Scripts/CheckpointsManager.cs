using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointsManager : MonoBehaviour
{
    private List<Checkpoint> _checkpoints = new List<Checkpoint>();
    private UnityEvent _levelFinished = new UnityEvent();

    public event UnityAction LevelFinished
    {
        add { _levelFinished.AddListener(value); }
        remove { _levelFinished.RemoveListener(value); }
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Checkpoint checkpoint = transform.GetChild(i).GetComponent<Checkpoint>();

            if (checkpoint)
                _checkpoints.Add(checkpoint);
        }
    }

    private void OnEnable()
    {
        foreach (Checkpoint currentCheckpoint in _checkpoints)
        {
            currentCheckpoint.Reached += OnCheckpointReached;
        }
    }

    private void OnDisable()
    {
        foreach (Checkpoint currentCheckpoint in _checkpoints)
        {
            currentCheckpoint.Reached -= OnCheckpointReached;
        }
    }

    private void OnCheckpointReached()
    {
        foreach (Checkpoint currentCheckpoint in _checkpoints)
        {
            if (!currentCheckpoint.IsReached)
                return;
        }

        _levelFinished.Invoke();
    }
}
