//=============================================================================
//
// �Q�[���I�[�o�[���o
//
// �쐬��:2022/03/16
// �쐬��:����T�q
//
// canvas�ɂ��̃X�N���v�g�����
//
// <�J������>
// 2022/03/16 �쐬
// 2022/03/20 ���o���̓T�u�J�����ɐ؂�ւ���悤�ɂ���
// 2022/03/24 CameraSwitch���炷���g�����J�����؂�ւ��ɕύX
// 2022/03/28 prefab����ǂݍ���ŕ\������悤�ɂ���
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //�Q�[���I�[�o�[�Ŏg���I�u�W�F�N�g
    //�摜
    private GameObject Image;
    //�v��n�u
    private GameObject prefab;
    //�L�����o�X
    Canvas canvas;
    //���C���J����
    //public GameObject mainCam;
    //�T�u�J����
    //public GameObject subCam;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

        prefab = (GameObject)Resources.Load("GameOverImage");
        Image = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        //canvas�̎q�ɐݒ�
        Image.transform.SetParent(this.canvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        //f4�L�[�������ꂽ��\���I��
        if (Input.GetKey(KeyCode.F4))
        {
            GameOverHide();
        }
    }
    public void GameOverShow()
    {
        //�摜�\��
        Image.GetComponent<ImageShow>().Show();
        //�J�����؂�ւ��@���C���J�������T�u�J�����@��Ԃ���
        //CameraSwitch.StartSwitching(mainCam, subCam, true);
        ////���C���J�����I�t
        //mainCam.SetActive(false);
        ////�T�u�J�����I��
        //subCam.SetActive(true);
    }
    public void GameOverHide()
    {
        //�摜����
        Image.GetComponent<ImageShow>().Hide();
        //�J�����؂�ւ� �T�u�J���������C���J�����@��ԂȂ�
        //CameraSwitch.StartSwitching(subCam, mainCam, false);
        ////�T�u�J�����I�t
        //subCam.SetActive(false);
        ////���C���J�����I��
        //mainCam.SetActive(true);
    }
}
