using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public static class GamePadManager
{
    public enum eGamePadType {
        XBOX = 0,
        DUALSHOCK4,

        ALLTYPE
    }

    [System.NonSerialized]
    public static bool onceTiltStick = false;

    //==========================================================================
    //
    //  ゲームパッドの十字に配置されたボタン、十字キー、スタートボタンがおされた
    //
    //==========================================================================
    public static bool PressAnyButton(eGamePadType gamepadtype) {
        if(GameData.gamepad == null)
        {
            return false;
        }

        if (gamepadtype == eGamePadType.XBOX)
        {
            if (GameData.gamepad.aButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.bButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.xButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.yButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.up.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.down.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.right.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.left.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.startButton.wasPressedThisFrame)
            {
                return true;
            }
        }
        else if(gamepadtype == eGamePadType.DUALSHOCK4)
        {
            if (GameData.gamepad.circleButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.crossButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.squareButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.triangleButton.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.up.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.down.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.right.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.left.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.startButton.wasPressedThisFrame)
            {
                return true;
            }

        }
        else if(gamepadtype == eGamePadType.ALLTYPE)
        {
            if (GameData.gamepad.buttonEast.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.buttonWest.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.buttonNorth.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.buttonSouth.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.up.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.down.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.right.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.left.wasPressedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.startButton.wasPressedThisFrame)
            {
                return true;
            }
        }
        return false;
    }

    //==========================================================================
    //
    //  ゲームパッドの十字に配置されたボタン、十字キー、スタートボタンが離された
    //
    //==========================================================================
    public static bool ReleaseAnyButton(eGamePadType gamepadtype) {
        if (GameData.gamepad == null)
        {
            return false;
        }

        if (gamepadtype == eGamePadType.XBOX)
        {
            if (GameData.gamepad.aButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.bButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.xButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.yButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.startButton.wasReleasedThisFrame)
            {
                return true;
            }

        }
        else if (gamepadtype == eGamePadType.DUALSHOCK4)
        {
            if (GameData.gamepad.circleButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.crossButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.squareButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.triangleButton.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.startButton.wasReleasedThisFrame)
            {
                return true;
            }

        }
        else if (gamepadtype == eGamePadType.ALLTYPE)
        {
            if (GameData.gamepad.buttonEast.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.buttonWest.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.buttonNorth.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.buttonSouth.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }
            if (GameData.gamepad.startButton.wasReleasedThisFrame)
            {
                return true;
            }

        }
        return false;
    }

}
