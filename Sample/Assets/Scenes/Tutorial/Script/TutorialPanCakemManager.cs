using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanCakemManager : MonoBehaviour
{
    public enum Boss1State {
        BOSS1_START = 0,    //ボスの開始
        BOSS1_BATTLE,      //ボスとのバトル中
        BOSS1_END,          //ボスを倒したとき
        BOSS1_DEAD,
    }
    static public Boss1State BossState;
    private int CountDown;
    private float CountDownMax = 5.0f;
    public GameObject Bossobj;
    public static GameObject Boss;
    //public static Vector3 BossPos;
    BossEntry Entry;
    private Vector3 WarpEFPoint;
    private bool PlayEffect = false;

    private void Awake() {
        //ボスを倒したかどうかの判定
        if (!GameData.isAliveBoss1)
        {
            //ボスの処理を何もしない処理入れる
            BossState = Boss1State.BOSS1_DEAD;
            return;
        }

        //Entry = GameObject.Find("BossStageManager").GetComponent<BossEntry>();

        //BossPos = GameObject.Find("BossPoint").transform.position;
        if (GameData.isAliveBoss1)
        {
            //Bossobj.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            //Boss = Instantiate(Bossobj, BossPos, Quaternion.Euler(0.0f, -90.0f, 0.0f));
            BossState = Boss1State.BOSS1_START;
        }
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start() {
        //Warp = GameObject.Find("MovePointToKitchen6");
        //Warp.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        switch (BossState)
        {
            case Boss1State.BOSS1_START:
                {

                    Entry.Entry();

                    break;
                }
            case Boss1State.BOSS1_BATTLE:
                {
                    //Boss.gameObject.transform.position = BossPos;
                    break;
                }
            case Boss1State.BOSS1_END:
                {
                    if (!PlayEffect)
                    {
                        EffectManager.Play(EffectData.eEFFECT.EF_BOSSKILL, new Vector3(Boss.transform.position.x, Boss.transform.position.y, Boss.transform.position.z), 8f);
                        PlayEffect = true;
                    }

                    Destroy(Boss, 7.9f);

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
