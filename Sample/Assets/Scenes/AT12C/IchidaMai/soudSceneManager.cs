using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soudSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ////----- âπñ¬ÇÁÇ∑èÄîı -----                                                       //----- âπñ¬ÇÁÇ∑èÄîı -----
        //for (int i = 0; i < SoundData.GameAudioList.Length; ++i)
        //{
        //    SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        //}
        //GameObject bgmObject = GameObject.Find("BGMObject(Clone)");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.Play(SoundData.eSE.SE_JUMP, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.Play(SoundData.eSE.SE_LAND, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SoundManager.Play(SoundData.eSE.SE_SHIELD, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.Play(SoundData.eSE.SE_REFLECTION_STAR, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SoundManager.Play(SoundData.eSE.SE_HEAL, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SoundManager.Play(SoundData.eSE.SE_PLAYER_DEATH, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SoundManager.Play(SoundData.eSE.SE_BOOS1_STRAWBERRY, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.Play(SoundData.eSE.SE_BUROKORI, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SoundManager.Play(SoundData.eSE.SE_NINJIN, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SoundManager.Play(SoundData.eSE.SE_TOMATO_BOMB, SoundData.GameAudioList);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //SoundManager.Play(SoundData.eSE.SE_TOMATO_BOUND, SoundData.GameAudioList);

            SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SoundManager.Play(SoundData.eSE.SE_GAMEOVER, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Play(SoundData.eSE.SE_GATEOPEN, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SoundManager.Play(SoundData.eSE.SE_EXTINGUISH, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SoundManager.Play(SoundData.eSE.SE_SWITCH, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SoundManager.Play(SoundData.eSE.SE_LASTBOSS_ULT, SoundData.GameAudioList);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.Play(SoundData.eSE.SE_LASTBOSS_BULLET, SoundData.GameAudioList);
        }

    }
}
