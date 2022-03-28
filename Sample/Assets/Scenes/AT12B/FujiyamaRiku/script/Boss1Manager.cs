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
