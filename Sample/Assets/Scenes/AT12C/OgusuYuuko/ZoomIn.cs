//=============================================================================
//
// �Y�[���C��
//
//�Q�[���I�[�o�[�p�ɍ����
//�w�肳�ꂽ�^�[�Q�b�g��Ǐ]����
//
// �쐬��:2022/03/20
// �쐬��:����T�q
//
// <�J������>
// 2022/03/20 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    //�^�[�Q�b�g�I�u�W�F�N�g
    public GameObject TargetObject;
    //�^�[�Q�b�g���猩���J�����ʒu
    public Vector3 cameraPos = new Vector3(0.0f, 0.0f, -2.0f);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = TargetObject.transform.position + cameraPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = TargetObject.transform.position + cameraPos;
    }
}
