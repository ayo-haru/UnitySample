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

public class Boss1Manager : MonoBehaviour
{
    public enum Boss1State
    { 
        BOSS1_START = 0,    //ボスの開始
        BOSS1_BATTLE ,      //ボスとのバトル中
        BOSS1_END,          //ボスを倒したとき
        BOSS1_DEAD,
    }
    static public Boss1State BossState;
    private int CountDown;
    private float CountDownMax = 5.0f;
    public Text timerText;
    public static GameObject Bossobj;
    public static GameObject Boss;
    public static Vector3 BossPos;
    private void Awake()
    {
        
        Bossobj = (GameObject)Resources.Load("PanCake");

        BossPos = GameObject.Find("BossPoint").transform.position;
        if (GameData.isAliveBoss1)
        {
            Bossobj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Boss = Instantiate(Bossobj, BossPos, Quaternion.Euler(0.0f, -90.0f, 0.0f));
            BossState = Boss1State.BOSS1_START;
        }
        Application.targetFrameRate = 60;
        //ボスを倒したかどうかの判定
        if (!GameData.isAliveBoss1)
        {
            //ボスの処理を何もしない処理入れる
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
                    Boss.gameObject.transform.position = BossPos;
                    break;
                }
            case Boss1State.BOSS1_END:
                {
                    Destroy(GameObject.Find("PanCake(Clone)"));
                    break;
                }
            case Boss1State.BOSS1_DEAD:
                {
                    break;
                }
        }
    }
}
