//=============================================================================
//
// �V�[���̂ɂ�����f�[�^�̃}�l�[�W��[PlayerScene2Manager]
//
// �쐬��:2022/03/11
// �쐬��:�g����
//
// <�J������>
// 2022/03/11 �쐬
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScene2Manager : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("SD_unitychan_humanoid");
        this.Player.transform.position = new Vector3(2.0f,0.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
