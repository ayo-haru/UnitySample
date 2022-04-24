//=============================================================================
//
// �L�b�`���}�b�v�}�l�[�W���[
//
// �쐬��:2022/04/22
// �쐬��:����T�q
//
// <�J������>
// 2022/04/22 �쐬
// 2022/04/24 ���@�w�ǉ�
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //�v���C���[�A�C�R��
    public GameObject[] PlayerIcon;
    //���@�w
    public GameObject MagicCircle;
    //��񂾂��Ăяo���p�̃t���O
    private bool OnceFlag = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        //�v���C���[�A�C�R�����\��
        for (int i = 0; i < PlayerIcon.Length; ++i)
        {
            PlayerIcon[i].SetActive(false);
        }

        OnceFlag = true;
    }
    private void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���@�w�\��
        MagicCircle.GetComponent<ImageShow>().Show(1000);
        if (!OnceFlag)
        {
            return;
        }

        //�V�[�����ŕ���@CurrentMapNumber���������炻�����ɕς���\��
        //switch (SceneManager.GetActiveScene().name)
        //{
        //    case "KitchenStage001":
        //        PlayerIcon[0].SetActive(true);
        //        break;
        //    case "KitchenStage002":
        //        PlayerIcon[1].SetActive(true);
        //        break;
        //    case "KitchenStage003":
        //        PlayerIcon[2].SetActive(true);
        //        break;
        //    case "KitchenStage004":
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //    case "KitchenStage005":
        //        PlayerIcon[4].SetActive(true);
        //        break;
        //    case "KitchenStage006":
        //        PlayerIcon[5].SetActive(true);
        //        break;
        //    default:
        //        PlayerIcon[3].SetActive(true);
        //        break;
        //}

        //CurrentMapNumber�����ɕ\������v���C���[�A�C�R���ݒ�
        //eSceneState���ύX����Ă��A�A�ԂȂ�[]�̒��ύX���Ȃ��Ă����悤�ɂ���
        PlayerIcon[GameData.CurrentMapNumber - (int)GameData.eSceneState.KitchenStage001].SetActive(true);


        OnceFlag = false;
    }
}


