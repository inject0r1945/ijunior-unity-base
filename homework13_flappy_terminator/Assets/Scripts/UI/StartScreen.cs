using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : Screen
{
    public event UnityAction PlayBattonClick;

    protected override void OnButtonClick()
    {
        PlayBattonClick.Invoke();
    }
}
