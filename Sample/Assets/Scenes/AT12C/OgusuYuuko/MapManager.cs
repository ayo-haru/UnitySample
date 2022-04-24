//=============================================================================
//
// �}�b�v�}�l�[�W���[
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
using UnityEngine.SceneManagement;

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
            PlayerIcon[i].GetComponent<ImageShow>().Hide();
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

        //�v���C���[������V�[���ɂ���ĕ\������v���C���[�A�C�R����ς���
        //switch (GameData.CurrentMapNumber)
        //{
        //    case (int)GameData.eSceneState.BOSS1_SCENE:
        //        PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        break;
        //    case (int)GameData.eSceneState.KitchenStage001:
        //        PlayerIcon[(int)MapNo.No_1to7].GetComponent<ImageShow>().Show();
        //        break;
        //    case (int)GameData.eSceneState.KitchenStage002:
        //        PlayerIcon[(int)MapNo.No_8to10].GetComponent<ImageShow>().Show();
        //        break;
        //    default:
        //        PlayerIcon[(int)MapNo.No_17].GetComponent<ImageShow>().Show();
        //        break;
        //        //case (int)GameData.eSceneState.BOSS1_SCENE:
        //        //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        //    break;
        //        //case (int)GameData.eSceneState.BOSS1_SCENE:
        //        //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        //    break;
        //        //case (int)GameData.eSceneState.BOSS1_SCENE:
        //        //    PlayerIcon[(int)MapNo.No_4].GetComponent<ImageShow>().Show();
        //        //    break;
        //}

        //�V�[�����ŕ���@CurrentMapNumber���������炻�����ɕς���\��
        switch (SceneManager.GetActiveScene().name)
        {
            case "KitchenStage001":
                PlayerIcon[0].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage002":
                PlayerIcon[1].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage003":
                PlayerIcon[2].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage004":
                PlayerIcon[3].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage005":
                PlayerIcon[4].GetComponent<ImageShow>().Show();
                break;
            case "KitchenStage006":
                PlayerIcon[5].GetComponent<ImageShow>().Show();
                break;
        }
        OnceFlag = false;
    }
}


