using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1KnifeThrow : MonoBehaviour
{
    Boss1Attack BossAttack;
    //----------------------------------------------------------
    //ナイフ投げ変数群
    //----------------------------------------------------------
    GameObject Knifeobj;                                    //ナイフ生成用
    GameObject Knife;                                       //ナイフ生成後格納
    Vector3 KnifeStartPoint;                                //ナイフのスタート座標
    Vector3 KnifeEndPoint;                                  //ナイフの終了地点
    Vector3 KnifePlayerPoint;
    float KnifeTime;
    [SerializeField] public float KnifeSpeed;               //ナイフの速度
    bool KnifeRefFlg = false;                               //ナイフが弾かれたかどうか
    float KnifeRefTime;
    Quaternion KnifeRotForward;                             //ナイフの角度変更用
    Quaternion KnifeRotDir;                                 //ナイフの角度変更用
    [SerializeField] Vector3 KnifeForward;                  //ナイフの前変更用
    Vector3 KnifePos;

    [SerializeField] public float KnifeThrowTime;
    float KnifeThrowNowTime;
    GameObject KnifeAimObj;
    GameObject KnifeAim;
    Vector3 KnifeAimPos;
    bool AimFlg;
    bool AimOnly;
    bool AimStart;
    // Start is called before the first frame update
    void Start()
    {
        Knifeobj = (GameObject)Resources.Load("Knife");
        KnifeAimObj = (GameObject)Resources.Load("KnifeAim");
        BossAttack = this.GetComponent<Boss1Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
            //それぞれの初期化をかける
            if (Knife != null)
            {
                Destroy(Knife);
            }
        }
    }

    public void Boss1Knife()
    {
        if (!BossAttack.AnimFlg)
        {
            BossAttack.AnimFlagOnOff();
            BossAttack.BossAnim.SetBool("TakeToKnife", true);
        }
        //ナイフ遠距離

        if (!AimFlg)
        {
            if (!AimOnly)
            {
                KnifeAim = Instantiate(KnifeAimObj, GameData.PlayerPos, Quaternion.Euler(90f, 0f, 0f));
                AimOnly = true;
            }
            KnifeAimPos.x = GameData.PlayerPos.x;
            KnifeAimPos.y = GameData.PlayerPos.y;
            KnifeAimPos.z = GameData.PlayerPos.z + 3.0f;

            if (KnifeThrowTime >= KnifeThrowNowTime)
            {
                KnifeAim.transform.position = KnifeAimPos;
                KnifeThrowNowTime += Time.deltaTime;
            }
            else
            {
                AimFlg = true;
                AimOnly = false;
                KnifeThrowNowTime = 0;
            }
        }

        if (AimStart)
        {
            if (!BossAttack.OnlyFlg)
            {
                BossAttack.OnlyFlg = true;
                GameObject.Find("knife(Clone)").transform.parent.DetachChildren();
                Vector3 KnifeDir = GameData.PlayerPos - Knife.transform.position;
                // ターゲットの方向への回転
                KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
                KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
            }
            if (BossAttack.RefrectFlg)
            {
                KnifeRefFlg = true;
                KnifePlayerPoint = KnifePos;
                Vector3 KnifeDir = Boss1Manager.Boss.gameObject.transform.position - Knife.transform.position;
                // ターゲットの方向への回転
                KnifeRotDir = Quaternion.LookRotation(KnifeDir, Vector3.back);
                KnifeRotForward = Quaternion.FromToRotation(KnifeForward, Vector3.forward);
                Knife.transform.rotation = KnifeRotDir * KnifeRotForward;
                BossAttack.RefrectFlg = false;
            }
            if (BossAttack.OnlyFlg && !KnifeRefFlg)
            {
                KnifeTime += Time.deltaTime * KnifeSpeed;
                Knife.transform.position = Vector3.Lerp(KnifeStartPoint, KnifeEndPoint, KnifeTime);
                KnifePos = GameObject.Find("knife(Clone)").transform.position;
                if (KnifeTime >= 1.0f)
                {
                    BossAttack.OnlyFlg = false;
                    AimFlg = false;
                    BossAttack.AnimFlagOnOff();
                    Debug.Log("Aパターンが通ったよー");
                    BossAttack.BossAnim.SetBool("TakeToKnife", false);
                    KnifeTime = 0;
                    AimOnly = false;
                    AimStart = false;
                    Destroy(Knife);
                    Destroy(KnifeAim);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }

            if (KnifeRefFlg)
            {
                KnifeRefTime += Time.deltaTime * 3;
                Knife.transform.position = Vector3.Lerp(KnifePlayerPoint, Boss1Manager.BossPos, KnifeRefTime);
                Debug.Log("Knife " + KnifePlayerPoint);
                if (KnifeRefTime >= 1.0f)
                {
                    BossAttack.HpScript.DelHP(BossAttack.KnifeDamage);
                    BossAttack.OnlyFlg = false;
                    KnifeRefFlg = false;
                    AimFlg = false;
                    BossAttack.BossAnim.SetBool("??ToDamage", true);
                    BossAttack.BossAnim.Play("Damage");
                    BossAttack.BossAnim.SetBool("??ToDamage", false);
                    BossAttack.BossAnim.SetBool("TakeToKnife", false);
                    BossAttack.AnimFlagOnOff();
                    AimOnly = false;
                    AimStart = false;
                    KnifeTime = 0;
                    KnifeRefTime = 0;
                    Destroy(KnifeAim);
                    SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);

                    Destroy(Knife);
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);

                    }
                    return;
                }
            }
        }

    }
    void StartShotAnim()
    {
        AimStart = true;
        BossAttack.WeaponAttackFlg = false;
        KnifeStartPoint = GameObject.Find("KnifePos").transform.position;
        KnifeEndPoint = GameData.PlayerPos;
        KnifePos = GameObject.Find("KnifePos").transform.position;
        Knife = Instantiate(Knifeobj, KnifePos, Quaternion.identity);
        Knife.transform.parent = GameObject.Find("KnifePos").transform;
        SoundManager.Play(SoundData.eSE.SE_BOOS1_KNIFE, SoundData.GameAudioList);
    }
}
