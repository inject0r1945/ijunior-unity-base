using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyController", menuName = "InputControllers/EmptyController")]
public class EmptyController : InputController
{
    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }

    public override bool RetrieveJumpInput()
    {
        return false;
    }

    public override float RetrieveMovementInput()
    {
        return 0f;
    }
}
