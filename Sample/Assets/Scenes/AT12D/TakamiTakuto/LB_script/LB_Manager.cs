using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_Manager : MonoBehaviour
{
    public enum LBState
    {
        LB_START = 0,    //ボスの開始
        LB_BATTLE,      //ボスとのバトル中
        LB_END,          //ボスを倒したとき
        LB_DEAD,
    }
    static public LBState BossState;
    private int CountDown;
    private float CountDownMax = 5.0f;
    //public Text timerText;
    public static GameObject Bossobj;
    public static GameObject Boss;
    public static Vector3 BossPos;
    private void Awake()
    {

        Bossobj = (GameObject)Resources.Load("LastBoss");

        BossPos = GameObject.Find("BossPoint").transform.position;
        if (GameData.isAliveBoss1)
        {
            Bossobj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Boss = Instantiate(Bossobj, BossPos, Quaternion.Euler(0.0f, -90.0f, 0.0f));
            BossState = LBState.LB_START;
        }
        Application.targetFrameRate = 60;
        //ボスを倒したかどうかの判定
        if (!GameData.isAliveBoss1)
        {
            //ボスの処理を何もしない処理入れる
            BossState = LBState.LB_DEAD;
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

        switch (BossState)
        {
            case LBState.LB_START:
                {

                    CountDownMax -= Time.deltaTime;
                    CountDown = (int)CountDownMax;

                    //timerText.text = CountDown.ToString();
                    if (CountDown <= 0)
                    {
                        //Debug.Log("Time : " + CountDownMax);
                        //timerText.enabled = false;
                        BossState = LBState.LB_BATTLE;

                    }
                    break;
                }
            case LBState.LB_BATTLE:
                {
                    Boss.gameObject.transform.position = BossPos;
                    break;
                }
            case LBState.LB_END:
                {
                    Destroy(GameObject.Find("LastBoss(Clone)"));
                    break;
                }
            case LBState.LB_DEAD:
                {
                    break;
                }
        }
    }
}
