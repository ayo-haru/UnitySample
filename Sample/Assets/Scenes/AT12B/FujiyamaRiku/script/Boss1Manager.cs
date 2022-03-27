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
using UnityEngine.UI;

public class Boss1Manager : MonoBehaviour
{
    public enum Boss1State
    { 
        BOSS1_START = 0,    //�{�X�̊J�n
        BOSS1_BATTLE ,      //�{�X�Ƃ̃o�g����
        BOSS1_END,          //�{�X��|�����Ƃ�
    }
    static public Boss1State BossState;
    private int CountDown;
    private float CountDownMax = 5.0f;
    public Text timerText;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        BossState = Boss1State.BOSS1_START;
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
        switch(BossState)
        {
            case Boss1State.BOSS1_START:
                {
                    
                    CountDownMax -= Time.deltaTime;
                    CountDown = (int)CountDownMax;
                    
                    timerText.text = CountDown.ToString();
                    if (CountDown <= 0)
                    {
                        //Debug.Log("Time : " + CountDownMax);
                        timerText.enabled = false;
                        BossState = Boss1State.BOSS1_BATTLE;
                        
                    }
                    break;
                }
            case Boss1State.BOSS1_BATTLE:
                {
                    
                    break;
                }
            case Boss1State.BOSS1_END:
                {
                    break;
                }
        }
    }
}
