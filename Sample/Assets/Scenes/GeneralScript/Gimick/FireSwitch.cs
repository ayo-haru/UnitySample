using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour
{

    private GameObject switch_ON;
    private MeshRenderer Mesh_On;
    private GameObject switch_OFF;
    private MeshRenderer Mesh_Off;
    private bool OnOff;

    // Start is called before the first frame update
    void Start()
    {
        switch_ON = GameObject.Find("switch_on");
        Mesh_On = switch_ON.GetComponent<MeshRenderer>();
        switch_OFF = GameObject.Find("switch_off");
        Mesh_Off = switch_OFF.GetComponent<MeshRenderer>();
        OnOff = true;

        Mesh_On.enabled = true;
        Mesh_Off.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ToggleButton()
    {
        OnOff = !OnOff;

        if (OnOff)
        {
            Mesh_On.enabled = true;
            Mesh_Off.enabled = false;

        }
        else
        {
            Mesh_On.enabled = false;
            Mesh_Off.enabled = true;
        }


        Debug.Log("onoff5"+OnOff);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")   // 盾と当たったらUIを変更して音だしてエフェクトだして消す
        {
            ToggleButton();
        }
    }

}



