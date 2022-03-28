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
        BOSS1_DEAD,
    }
    static public Boss1State BossState;
    private int CountDown;
    private float CountDownMax = 5.0f;
    public Text timerText;
    GameObject Bossobj;
    private void Awake()
    {
        
        Bossobj = (GameObject)Resources.Load("Boss");
        if (GameData.isAliveBoss1)
        {
            Instantiate(Bossobj, Boss1Attack.BossStartPoint, Quaternion.identity);
            BossState = Boss1State.BOSS1_START;
        }
        Application.targetFrameRate = 60;
        //�{�X��|�������ǂ����̔���
        if (!GameData.isAliveBoss1)
        {
            //�{�X�̏������������Ȃ����������
            BossState = Boss1State.BOSS1_DEAD;
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
                    if (!GameData.isAliveBoss1)
                    {
                        BossState = Boss1State.BOSS1_END;
                    }
                    break;
                }
            case Boss1State.BOSS1_END:
                {
                    Destroy(GameObject.Find("Boss(Clone)"));
                    break;
                }
            case Boss1State.BOSS1_DEAD:
                {
                    break;
                }
        }
    }
}
