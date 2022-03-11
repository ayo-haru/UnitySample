//=============================================================================
//
// �V�[���J��[MoveMap]
//
// �쐬��:2022/03/09
// �쐬��:�g����
//
// <�J������>
// 2022/03/09 �쐬
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //---�V�[����؂�ւ��Ă��j�󂵂Ȃ����̂�����
        DontDestroyOnLoad(GameObject.Find("SD_unitychan_humanoid"));
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        // �^�O�ŃI�u�W�F�N�g�𔻒f����B
        if (other.gameObject.tag == "MovePoint1to2")
        //if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("PlayerScene2");
        }

        else if (other.gameObject.tag == "MovePoint2to1")
        //if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("PlayerScene1");
        }

    }
}
