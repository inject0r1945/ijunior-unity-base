using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputControllers/PlayerController")]
public class PlayerController : InputController
{
    private const string HorizontalAxisName = "Horizontal";
    private const string JumpKeysName = "Jump";

    public override float RetrieveMovementInput()
    {
        return Input.GetAxis(HorizontalAxisName);
    }

    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown(JumpKeysName);
    }

    public override bool RetrieveJumpHoldInput()
    {
        return Input.GetButton(JumpKeysName);
    }
}
