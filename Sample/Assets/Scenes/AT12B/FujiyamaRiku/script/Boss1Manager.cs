//==========================================================
//      �{�X�̃Q�[�W�쐬
//      �쐬���@2022/03/14
//      �쐬�ҁ@���R����
//      
//      <�J������>
//      2022/03/14      �{�X�}�l�[�W���[�̍쐬  
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour
{
    public enum Boss1State
    { 
        BOSS1_START = 0,    //�{�X�̊J�n
        BOSS1_BATTLE ,      //�{�X�Ƃ̃o�g����
        BOSS1_END,          //�{�X��|�����Ƃ�
    }
    private void Awake()
    {
        //�{�X��|�������ǂ����̔���
        if (!GameData.isAliveBoss1)
        {
            //�{�X�̏������������Ȃ����������
            return;
        }
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
