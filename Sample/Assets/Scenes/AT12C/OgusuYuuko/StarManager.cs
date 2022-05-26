//=============================================================================
//
// �X�^�[���Ǘ�����N���X
//
// �쐬��:2022/05/07
// �쐬��:����T�q
//
// <�J������>
// 2022/05/07 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    //�V�[���ɂ���X�^�[
    private GameObject[] starList;

    // Start is called before the first frame update
    void Start()
    {
        //�V�[���ɂ����Ă���X�^�[���擾
        starList = GameObject.FindGameObjectsWithTag("Item");
        //�I�u�W�F�N�g���擾����鏇�Ԃ�������Ȃ����߁A�����W�̈ʒu�ŕ��ёւ���
        for (int i = 0; i < starList.Length - 1; ++i)
        {
            for (int j = i + 1; j < starList.Length; ++j)
            {
                if (starList[i].transform.position.x > starList[j].transform.position.x)
                {
                    GameObject work = starList[i];
                    starList[i] = starList[j];
                    starList[j] = work;
                }else if(starList[i].transform.position.x == starList[j].transform.position.x)
                {//x���W�������������ꍇ�Ay���W�̈ʒu�ŕ��ёւ���
                    if(starList[i].transform.position.y > starList[j].transform.position.y)
                    {
                        GameObject work = starList[i];
                        starList[i] = starList[j];
                        starList[j] = work;
                    }
                }
            }
        }

        //�L�b�`���V�[���P�`�U�̏ꍇ
        if(GameData.CurrentMapNumber >= (int)GameData.eSceneState.KitchenStage001 && GameData.CurrentMapNumber <= (int)GameData.eSceneState.KitchenStage006)
        {
            for (int i = 0; i < starList.Length; ++i)
            {
                //true��������擾�ς݂Ȃ̂ŏ���
                if (GameData.isStarGet[GameData.CurrentMapNumber - 1, i])
                {
                    Destroy(starList[i]);
                    continue;
                }
                //�X�^�[�Ɏ��ʗpid���蓖��
                starList[i].GetComponent<Star>().SetID(i);

            }
        }
        

        //�X�e�[�W1��id��3,4�̐��̓X�e�[�W�R��id��0,1�̐��Ɠ������߁A�X�e�[�W�R�̐��擾�󋵂��Q��
        if(GameData.CurrentMapNumber == (int)GameData.eSceneState.KitchenStage001)
        {
            if(GameData.isStarGet[(int)GameData.eSceneState.KitchenStage003 - 1, 0])
            {
                Destroy(starList[2]);
            }
            if (GameData.isStarGet[(int)GameData.eSceneState.KitchenStage003 - 1, 1])
            {
                Destroy(starList[3]);
            }
        }

        //�`���[�g���A���R�ꍇ
        if(GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial3)
        {
            //�X�^�[���ʗpid���蓖��
            starList[0].GetComponent<Star>().SetID(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
