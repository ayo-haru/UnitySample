using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    private GameObject switch_Close;
    private MeshRenderer Mesh_Close;
    private GameObject switch_Open;
    //private MeshRenderer Mesh_Open;
    private GameObject gate;
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        switch_Close = GameObject.Find("switch_close");
        Mesh_Close = switch_Close.GetComponent<MeshRenderer>();
        switch_Open = GameObject.Find("switch_open");
        //Mesh_Open = switch_Open.GetComponent<MeshRenderer>();

        gate = GameObject.Find("GateToStage1");
        if (!gate)
        {
            gate = GameObject.Find("GateToBoss1");
        }

        if (GameData.GateOnOff)
        {
            Mesh_Close.enabled = true;
            switch_Open.SetActive(false);
            animator.Play("Off");
        }
        else
        {
            Mesh_Close.enabled = false;
            switch_Open.SetActive(true);
            animator.Play("On");

            //Mesh_Close.enabled = false;
            //Mesh_Open.enabled = true;        
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ToggleButton() {
        GameData.GateOnOff = !GameData.GateOnOff;
        SaveManager.saveGateOnOff(GameData.GateOnOff);

        if (GameData.GateOnOff)
        {
            animator.Play("Off");
            StartCoroutine("WaitOff");
            //Mesh_Close.enabled = true;
            //Mesh_Open.enabled = false;
            SoundManager.Play(SoundData.eSE.SE_SWITCH, SoundData.GameAudioList);
            SoundManager.Play(SoundData.eSE.SE_GATEOPEN, SoundData.GameAudioList);

            if (gate)
            {
                gate.GetComponent<Gate>().Close();
            }
        }
        else
        {
            //Mesh_Close.enabled = false;
            //Mesh_Open.enabled = true;
            Mesh_Close.enabled = false;
            switch_Open.SetActive(true);
            animator.Play("On");

            SoundManager.Play(SoundData.eSE.SE_SWITCH, SoundData.GameAudioList);
            SoundManager.Play(SoundData.eSE.SE_GATEOPEN, SoundData.GameAudioList);
            if (gate)
            {
                gate.GetComponent<Gate>().Open();
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Weapon")   // ���Ɠ���������UI��ύX���ĉ������ăG�t�F�N�g�����ď���
        {
            ToggleButton();
        }
    }

    private IEnumerator WaitOff() {
        for (int i = 32; i > 1; i--)
        {
            yield return null;
        }
        Mesh_Close.enabled = true;
        switch_Open.SetActive(false);
    }

}