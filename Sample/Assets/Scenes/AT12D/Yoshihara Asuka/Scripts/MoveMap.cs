//=============================================================================
//
// �}�b�v�J��[MoveMap]
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
        DontDestroyOnLoad(GameObject.Find("SD_unitychan_humanoid"));        // �V�[����؂�ւ��Ă��j�󂵂Ȃ�
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovePoint")
        {
            SceneManager.LoadScene("PlayerScene2");
        }
    }
}
