using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour {

    private GameObject switch_ON;
    private MeshRenderer Mesh_On;
    private GameObject switch_OFF;
    private MeshRenderer Mesh_Off;
    private GameObject stove;
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        switch_ON = GameObject.Find("switch_on");
        //Mesh_On = switch_ON.GetComponent<MeshRenderer>();
        switch_OFF = GameObject.Find("switch_off");
        Mesh_Off = switch_OFF.GetComponent<MeshRenderer>();

        //stove = GameObject.Find("Stove");

        if (GameData.FireOnOff)
        {
            //---On�̎�
            //Mesh_On.enabled = true;
            //Mesh_Off.enabled = false;
            switch_ON.SetActive(true);
            Mesh_Off.enabled = false;
            //stove.GetComponent<Stove>().Ignition();
            animator.Play("On");
        }
        else
        {
            //---Off�̎�
            //Mesh_On.enabled = false;
            //Mesh_Off.enabled = true;
            //stove.GetComponent<Stove>().Extinguish();
            switch_ON.SetActive(false);
            Mesh_Off.enabled = true;

            animator.Play("Off");
        }

    }

    // Update is called once per frame
    void Update() {
    }

    private void ToggleButton() {
        GameData.FireOnOff = !GameData.FireOnOff;
        SaveManager.saveFireOnOff(GameData.FireOnOff);

        if (GameData.FireOnOff)
        {
            //Mesh_On.enabled = true;
            switch_ON.SetActive(true);
            Mesh_Off.enabled = false;
            SoundManager.Play(SoundData.eSE.SE_SWITCH, SoundData.GameAudioList);
            animator.Play("On");

        }
        else
        {
            //Mesh_On.enabled = false;
            SoundManager.Play(SoundData.eSE.SE_EXTINGUISH, SoundData.GameAudioList);
            SoundManager.Play(SoundData.eSE.SE_SWITCH, SoundData.GameAudioList);
            animator.Play("Off");
            StartCoroutine("WaitOff");
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
        switch_ON.SetActive(false);
        Mesh_Off.enabled = true;
    }

}



