using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    private GameObject switch_Close;
    private MeshRenderer Mesh_Close;
    private GameObject switch_Open;
    private MeshRenderer Mesh_Open;
    private GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        switch_Close = GameObject.Find("switch_close");
        Mesh_Close = switch_Close.GetComponent<MeshRenderer>();
        switch_Open = GameObject.Find("switch_open");
        Mesh_Open = switch_Open.GetComponent<MeshRenderer>();

        gate = GameObject.Find("GateToStage1");
        if (!gate)
        {
            gate = GameObject.Find("GateToBoss1");
        }

        if (GameData.GateOnOff)
        {
            Mesh_Close.enabled = true;
            Mesh_Open.enabled = false;
        }
        else
        {
            Mesh_Close.enabled = false;
            Mesh_Open.enabled = true;        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ToggleButton() {
        GameData.GateOnOff = !GameData.GateOnOff;

        if (GameData.GateOnOff)
        {
            Mesh_Close.enabled = true;
            Mesh_Open.enabled = false;
            SoundManager.Play(SoundData.eSE.SE_SWITCH, SoundData.GameAudioList);
            if (gate)
            {
                gate.GetComponent<Gate>().Close();
            }
        }
        else
        {
            Mesh_Close.enabled = false;
            Mesh_Open.enabled = true;
            SoundManager.Play(SoundData.eSE.SE_GATEOPEN, SoundData.GameAudioList);
            if (gate)
            {
                gate.GetComponent<Gate>().Open();
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Weapon")   // 盾と当たったらUIを変更して音だしてエフェクトだして消す
        {
            ToggleButton();
        }
    }

}
