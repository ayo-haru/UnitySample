//=============================================================================
//
// �J�����R���g���[��
//
// �쐬��:2022/04/17
// �쐬��:�g����
//
// <�J������>
// 2022/04/17   �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //---�ϐ��錾
    public GameObject Player;           // �Ǐ]����I�u�W�F�N�g�ϐ�
    private Vector3 Offset;            // �Ǐ]����I�u�W�F�N�g�ƃJ�����̈ʒu�֌W��ۑ�

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n���Ƀv���C���[�ƃJ�����̈ʒu��ۑ�
        Offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //�@�v���C���[�̌��݂̈ʒu����V�����J�����̈ʒu���쐬
        Vector3 vector = Player.transform.position + Offset;

        // �c�����͌Œ�
        vector.y = transform.position.y;

        // �J�����̈ʒu���ړ�
        transform.position = vector;
    }
}
