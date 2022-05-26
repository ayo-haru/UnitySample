using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rush : MonoBehaviour
{
    Boss1Attack BossAttack;
    //突進用変数群
    //----------------------------------------------------------
    GameObject Forkobj;                                     //フォークのオブジェクト生成用
    GameObject Fork;                                        //フォークのオブジェクト格納用
    Vector3 RushStartPoint;                                 //突進開始地点
    Vector3 RushEndPoint;                                   //突進終了地点
    Vector3 RushPlayerPoint;                                //突進をはじいたときのプレイヤー座標格納用
    Vector3 RushRefEndPoint;                                //突進をはじいた後の敵の最終地点
    Vector3 RushMiddlePoint;                                //突進攻撃後戻ってくるための中間座標
    Vector3 ForkPos;
    bool OnlyRushFlg;                                       //一回限定
    [SerializeField] public float RushSpeed;                //突進のスピード
    bool RushRefFlg = false;                                //突進をはじいた判定
    float RushTime;                                         //突進の経過時間
    float RushRefTime;                                      //弾いた後の時間経過
    bool BossReturnFlg;
    float BossReturnTime;                                   //突進後戻るまでの時間
    bool RushEndFlg;
    float RushReturnSpeed;
    bool ReturnDelay;                                      //戻ろうとするまでの時間
    bool EFFlg;
    bool EFDelFlg;
    Vector3 oldScale;
    [SerializeField] public float RotateSpeed;
    Vector3 Rotate;
    // Start is called before the first frame update
    void Start()
    {
        Forkobj = (GameObject)Resources.Load("Fork");
        BossAttack = this.GetComponent<Boss1Attack>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameData.isAliveBoss1)
        {
            //それぞれの初期化をかける
            if (Fork != null)
            {
                Destroy(Fork);
            }
            
        }
    }

    public void Boss1Fork()
    {
        //アニメーション再生
        if (!BossAttack.AnimFlg)
        {
            BossAttack.AnimFlagOnOff();
            BossAttack.BossAnim.SetBool("IdleToTake", true);
            //BossTakeCase = BossMove.Boss_State.charge;
        }
        //一回の処理が終わっていたら開始

        if (BossAttack.OnlyFlg && BossAttack.MoveFlg)
        {
            if (!EFFlg)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_BOSS_FORK, GameObject.Find("ForkEF").transform.position);
                EffectManager.Play(EffectData.eEFFECT.EF_BOOS_FORK_DUST, GameObject.Find("ForkEF").transform.position);
                if (BossAttack.RFChange)
                {
                    GameObject.Find("Boss_Fork2(Clone)").transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                }
                
                EFFlg = true;
            }
            //ボスが突進終了後に変える処理
            if (BossReturnFlg)
            {
                BossAttack.RefrectFlg = false;
                BossReturnTime += Time.deltaTime * RushReturnSpeed;
                //最後まで攻撃し終わっていたら
                if (RushEndFlg)
                {
                    Debug.Log("あああああああああああああああああああああああ！！！");
                    //方向が変わってたらスケールｘを反転
                    Boss1Manager.Boss.transform.localScale = BossAttack.Scale;
                    //回転の目標値
                    Quaternion target = new Quaternion();
                    //向きを設定
                    target = Quaternion.LookRotation(Rotate);
                    //ゆっくり回転させる
                    Boss1Manager.Boss.transform.rotation = Quaternion.RotateTowards(Boss1Manager.Boss.transform.rotation, target, RotateSpeed);
                }

                //途中で弾かれていたら
                if (!RushEndFlg)
                {
                    Boss1Manager.BossPos = Vector3.Lerp(RushRefEndPoint, RushStartPoint, BossReturnTime);
                }
               
                //開始地点まで戻ってきたときにもろもろ初期化
                if (BossReturnTime >= 1.0f)
                {
                    GameObject.Find("BossStageManager").GetComponent<ShakeCamera>().Shake(0.2f, 10, 1);
                    if (Fork != null)
                    {
                        Destroy(Fork);
                    }
                    if (RushEndFlg)
                    {
                        if (!BossAttack.RFChange)
                        {
                            BossAttack.RFChange = true;
                        }
                        else if (BossAttack.RFChange)
                        {
                            BossAttack.RFChange = false;
                        }
                    }
                    EFDelFlg = false;
                    ReturnDelay = false;
                    RushEndFlg = false;
                    BossReturnFlg = false;
                    RushRefFlg = false;
                    EFFlg = false; 
                    BossAttack.AnimFlagOnOff();
                    BossAttack.BossAnim.SetBool("IdleToTake", false);
                    BossAttack.BossAnim.SetBool("RushToJump", false);
                    BossAttack.AnimMoveFlgOnOff();
                    BossReturnTime = 0;
                    BossAttack.BossAnim.speed = 1;
                    if (HPgage.currentHp >= 50)
                    {
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    if (HPgage.currentHp < 50)
                    {
                        Debug.Log("アイドルぅ！！！！！！！！！！！");
                        BossMove.AttackCount += 1;
                        BossMove.SetState(BossMove.Boss_State.idle);
                    }
                    BossAttack.OnlyFlg = false;
                }
                return;
            }
            //弾かれたら一回だけ処理する部分
            if (BossAttack.RefrectFlg)
            {
                RushRefFlg = true;
                RushPlayerPoint = Boss1Manager.Boss.transform.position;
                BossAttack.BossAnim.SetBool("RushToJump", false);
                BossAttack.BossAnim.SetBool("Blow", true);
                BossAttack.RefrectFlg = false;
            }
            //弾かれていなかった場合の処理
            if (!RushRefFlg)
            {
                if (!BossAttack.RFChange)
                {
                    GameObject.Find("Boss_Fork2(Clone)").transform.position = GameObject.Find("ForkEF").transform.position;
                }
                if (BossAttack.RFChange)
                {
                    GameObject.Find("Boss_Fork2(Clone)").transform.position = GameObject.Find("LForkEF").transform.position;
                }
                RushTime += Time.deltaTime * RushSpeed;
                
                Boss1Manager.BossPos = Vector3.Lerp(RushStartPoint, RushEndPoint, RushTime);

                if (RushTime >= 1.0f)
                {
                    Destroy(GameObject.Find("Boss_Fork2(Clone)"));
                    EFDelFlg = true;
                    BossAttack.Scale.x *= -1;
                    RushReturnSpeed = 1f;
                    RushEndFlg = true;
                    BossReturnFlg = true;
                    RushTime = 0;
                    return;
                }
            }
            //弾かれていた場合の処理
            if (RushRefFlg)
            {
                
                RushRefTime += Time.deltaTime * 2f;
                //壁にぶつけているように見せる
                Boss1Manager.BossPos = Vector3.Lerp(RushPlayerPoint, RushRefEndPoint, RushRefTime);
                if (RushRefTime >= 1.0f)
                {
                    BossAttack.DamageColor.Invoke("Play" ,0.0f);
                    if (RushRefTime <= 1.1f)
                    {
                        GameObject.Find("BossStageManager").GetComponent<ShakeCamera>().Shake(0.3f, 5, 1);
                    }

                    Destroy(Fork);
                    RushEndFlg = false;
                    BossAttack.BossAnim.SetBool("Blow", false);
                    BossAttack.BossAnim.SetTrigger("WallHit");
                    BossAttack.BossAnim.Play("WallHit");
                    BossAttack.BossAnim.speed = 0.3f;
                    if (ReturnDelay)
                    {
                        RushReturnSpeed = 3f;
                        RushRefFlg = false;
                        BossReturnFlg = true;
                        BossAttack.BossAnim.SetBool("IdleToTake", false);
                        BossAttack.BossAnim.SetBool("RushToJump", false);
                        BossAttack.HpScript.DelHP(BossAttack.RushDamage);
                        RushTime = 0;
                        RushRefTime = 0;
                        SoundManager.Play(SoundData.eSE.SE_BOOS1_DAMEGE, SoundData.GameAudioList);
                        return;
                    }
                }
            }
        }

    }
    void ReturnGround()
    {
        ReturnDelay = true;
    }
    void BossRushAnim()
    {
        //突進攻撃を始めるために毎回一回だけ処理する部分
        if (!BossAttack.OnlyFlg)
        {
            BossAttack.WeaponAttackFlg = false;
            //右から左
            if (!BossAttack.RFChange)
            {
                
                BossAttack.OnlyFlg = true;
                Debug.Log("Pos : " + Boss1Manager.BossPos);
                RushStartPoint = GameObject.Find("BossPoint").transform.position;
                RushEndPoint = GameObject.Find("LeftBossPoint").transform.position;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = GameObject.Find("ForkRefEndPoint").transform.position;
                Rotate.x = 1;
                BossAttack.BossAnim.SetTrigger("TakeToRushTr");
                BossAttack.BossAnim.SetBool("RushToJump", true);
                BossAttack.BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
            //左から右
            else if (BossAttack.RFChange)
            {
                BossAttack.OnlyFlg = true;
                RushStartPoint = GameObject.Find("LeftBossPoint").transform.position;
                RushEndPoint = GameObject.Find("BossPoint").transform.position;
                ForkPos = GameObject.Find("ForkPos").transform.position;
                Fork = Instantiate(Forkobj, ForkPos, Quaternion.Euler(GameObject.Find("ForkPos").transform.rotation.eulerAngles));
                Fork.transform.parent = GameObject.Find("ForkPos").transform;
                RushRefEndPoint = GameObject.Find("LeftForkRefEndPoint").transform.position;
                Rotate.x = -1;
                BossAttack.BossAnim.SetTrigger("TakeToRushTr");
                BossAttack.BossAnim.SetBool("RushToJump", true);
                BossAttack.BossAnim.SetBool("IdleToTake", false);
                SoundManager.Play(SoundData.eSE.SE_BOOS1_DASHU, SoundData.GameAudioList);
            }
        }
    }
}
