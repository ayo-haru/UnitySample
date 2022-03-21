//=============================================================================
//
// �e�V�[���̃f�[�^�Ǘ�[Scene1Manager]
//
// �쐬��:2022/03/11
// �쐬��:�g����
//
// <�J������>
// 2022/03/11
// 2022/03/22   GameData����
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public GameObject playerPrefab;                     // �v���C���[�̃v���n�u������

    private void Awake()
    {
        Application.targetFrameRate = 60;

        //---�v���C���[�v���n�u�̎擾
        if (!GameData.Player)
        {
            GameData.Player = playerPrefab;             // GameData�̃v���C���[�Ɏ擾
        }
        GameData.PlayerPos = GameData.Player.transform.position = new Vector3(2.0f, 2.0f, -1.0f); // �v���C���[�̍��W��ݒ�
        GameObject player = Instantiate(GameData.Player);    // �v���n�u�����̉�

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
