//==========================================================
//      ボスのゲージ作成
//      作成日　2022/03/14
//      作成者　藤山凌希
//      
//      <開発履歴>
//      2022/03/14      ボスマネージャーの作成  
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
        LB_START = 0,    //ボスの開始
        LB_BATTLE,      //ボスとのバトル中
        LB_END,          //ボスを倒したとき
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
    public LastHPGage HpScript;                             //HPgage     
    EndingManager Script;

    private void Awake()
    {
        //ボスを倒したかどうかの判定
        
        Entry = GameObject.Find("LastBoss_Manager").GetComponent<LB_Entry>();
        //Track = GameObject.Find("LastBoss_Manager").GetComponent<LB_Trac>();
        LB_obj = (GameObject)Resources.Load("LastBoss");
        LB_Pos = GameObject.Find("LB_Point").transform.position;
        LBShot_obj= (GameObject)Resources.Load("LB_ShotPoint");
        LBShot_Pos = GameObject.Find("LB_Point").transform.position;
        LBShot_Pos.y = LBShot_Pos.y - 10;
        LBShot_Pos.z = GameData.PlayerPos.z;
        HpScript = GameObject.Find("HPGage").GetComponent<LastHPGage>();
        Script = GameObject.Find("EndingManager").GetComponent<EndingManager>();

        
            LB = Instantiate(LB_obj, LB_Pos, Quaternion.Euler(0.0f, -180.0f, 0.0f));
            LBShot = Instantiate(LBShot_obj, LBShot_Pos, Quaternion.identity);
            //LBShot.transform.parent = LB.transform;
            LB_States = LB_State.LB_START;
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
                    LBShot_Pos.x = LB.transform.position.x;
                    LBShot_Pos.y = LB.transform.position.y;

                    LBShot.transform.position = LBShot_Pos;
                    if(LastHPGage.currentHp <= 0)
                    {
                        LB_States = LB_State.LB_END;
                    }
                    //Track.enabled = true;
                    //LB.gameObject.transform.position = LB_Pos;
                    break;
                }
            case LB_State.LB_END:
                {
                    
                    //GetComponent<LB_DeathCam>().DeathCamera();
                    Destroy(LB);
                    Script.startFlag = true;
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

