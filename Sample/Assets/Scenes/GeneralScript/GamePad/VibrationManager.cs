//=============================================================================
//
// ControllerU“®
//
// ì¬“ú:2022/04/01
// ì¬Ò:‹gŒ´”ò’¹
//
// <ŠJ”­—š—ğ>
// 2022/04/01   ì¬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        Gamepad gamepad = Gamepad.current;
        if(gamepad != null)
        {
            gamepad.SetMotorSpeeds(0.0f,0.1f);
            yield return new WaitForSeconds(1.0f);
            gamepad.SetMotorSpeeds(0.0f,0.0f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
