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
    public GameObject Bossobj;
    public static GameObject Boss;
    public static Vector3 BossPos;
    private GameObject Warp;
    BossEntry Entry;
    BossTrac Track;
    private Vector3 WarpEFPoint;
    private bool PlayEffect = false;
    
    private void Awake()
    {
        //�{�X��|�������ǂ����̔���
        if (!GameData.isAliveBoss1)
        {
            //�{�X�̏������������Ȃ����������
            BossState = Boss1State.BOSS1_DEAD;
            return;
        }
        Entry = GameObject.Find("BossStageManager").GetComponent<BossEntry>();
        Track = GameObject.Find("BossStageManager").GetComponent<BossTrac>();
        Bossobj = (GameObject)Resources.Load("PanCake");

        BossPos = GameObject.Find("BossPoint").transform.position;
        if (GameData.isAliveBoss1)
        {
            //Bossobj.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            Boss = Instantiate(Bossobj, BossPos, Quaternion.Euler(0.0f, -90.0f, 0.0f));
            BossState = Boss1State.BOSS1_START;
        }
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Warp = GameObject.Find("MovePointToKitchen6");
        //Warp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(BossState)
        {
            case Boss1State.BOSS1_START:
                {
                    
                    Entry.Entry();
                   
                    break;
                }
            case Boss1State.BOSS1_BATTLE:
                {
                    Track.enabled = true;
                    Boss.gameObject.transform.position = BossPos;
                    break;
                }
            case Boss1State.BOSS1_END:
                {
                    GetComponent<BossTrac>().enabled = false;
                    if (!PlayEffect)
                    {
                        EffectManager.Play(EffectData.eEFFECT.EF_BOSS_DEATH,new Vector3(Boss.transform.position.x, Boss.transform.position.y, Boss.transform.position.z),8f);
                        PlayEffect = true;
                    }
                    GetComponent<BossDeathCam>().DeathCamera();
                     Destroy(Boss,7.9f);

                    //Warp.SetActive(true);
                    //WarpEFPoint = Warp.transform.position;
                    //WarpEFPoint.y = 11.7f;
                    //EffectManager.Play(EffectData.eEFFECT.EF_DARKAREA, WarpEFPoint);
                    break;
                }
            case Boss1State.BOSS1_DEAD:
                {
                    break;
                }
        }
    }
}
