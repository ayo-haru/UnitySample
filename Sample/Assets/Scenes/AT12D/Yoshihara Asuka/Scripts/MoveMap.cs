//=============================================================================
//
// �}�b�v�J��[MoveMap]
//
// �쐬��:2022/03/09
// �쐬��:�g����
//
// <�J������>
// 2022/03/09 �쐬
// 2022/03/11 �j�󂵂Ȃ��I�u�W�F�N�g��ǉ�
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
        //---�J�ڌ���j�󂵂Ȃ��I�u�W�F�N�g
        DontDestroyOnLoad(GameObject.Find("SD_unitychan_humanoid"));
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovePoint1to2")
        {
            SceneManager.LoadScene("PlayerScene2");
        }
    }
}
