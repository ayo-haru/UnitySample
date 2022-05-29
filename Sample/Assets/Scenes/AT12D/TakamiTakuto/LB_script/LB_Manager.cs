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

public class LB_Manager : MonoBehaviour
{
    public enum LB_State
    {
        LB_START = 0,    //�{�X�̊J�n
        LB_BATTLE,      //�{�X�Ƃ̃o�g����
        LB_END,          //�{�X��|�����Ƃ�
        LB_DEAD,
    }
    static public LB_State LB_States;
    private int CountDown;
    private float CountDownMax = 5.0f;
    public Text timerText;
    public GameObject LB_obj;
    public static GameObject LB;
    public static Vector3 LB_Pos;
    public  GameObject LBShot;
    public static GameObject LBShot_obj;
    public static Vector3 LBShot_Pos;
    private GameObject Warp;
    LB_Entry Entry;
    //LB_Trac Track;
    private Vector3 WarpEFPoint;
    private bool PlayEffect = false;

    private void Awake()
    {
        //�{�X��|�������ǂ����̔���
        if (!GameData.isAliveBoss1)
        {
            //�{�X�̏������������Ȃ����������
            LB_States = LB_State.LB_DEAD;
            return;
        }
        Entry = GameObject.Find("LastBoss_Manager").GetComponent<LB_Entry>();
        //Track = GameObject.Find("LastBoss_Manager").GetComponent<LB_Trac>();
        LB_obj = (GameObject)Resources.Load("LastBoss");
        LB_Pos = GameObject.Find("LB_Point").transform.position;
        LBShot_obj= (GameObject)Resources.Load("LB_ShotPoint");
        LBShot_Pos = GameObject.Find("LB_Point").transform.position;
        LBShot_Pos.z = 0.0f;
        transform.position = LBShot_Pos;

        if (GameData.isAliveBoss1)
        {
            //LB_obj.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            LB = Instantiate(LB_obj, LB_Pos, Quaternion.Euler(0.0f, -180.0f, 0.0f));
            LBShot = Instantiate(LBShot_obj, LBShot_Pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
            LBShot.transform.parent = LB.transform;
            LB_States = LB_State.LB_START;
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

        switch (LB_States)
        {
            case LB_State.LB_START:
                {

                    Entry.Entry();

                    break;
                }
            case LB_State.LB_BATTLE:
                {
                    //Track.enabled = true;
                    //LB.gameObject.transform.position = LB_Pos;
                    break;
                }
            case LB_State.LB_END:
                {
                    if (!PlayEffect)
                    {
                        //EffectManager.Play(EffectData.eEFFECT.EF_LB__DEATH, new Vector3(LB_.transform.position.x, LB_.transform.position.y, LB_.transform.position.z), 8f);
                        //PlayEffect = true;
                    }
                    //GetComponent<LB_DeathCam>().DeathCamera();
                    Destroy(LB, 7.9f);

                    //Warp.SetActive(true);
                    //WarpEFPoint = Warp.transform.position;
                    //WarpEFPoint.y = 11.7f;
                    //EffectManager.Play(EffectData.eEFFECT.EF_DARKAREA, WarpEFPoint);
                    break;
                }
            case LB_State.LB_DEAD:
                {
                    break;
                }
        }
    }
}

