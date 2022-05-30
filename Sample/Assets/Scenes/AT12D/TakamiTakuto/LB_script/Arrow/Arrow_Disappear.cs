using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Disappear : MonoBehaviour
{
    public GameObject ArrowRight;
    public GameObject ArrowLeft;
    public GameObject ArrowUp;
    public Vector3 ArrowUpinitpos;
    public int ArrowDisapperCount;
    public bool Attackspace=false;
    [SerializeField]  public int Arrow_MaxCount;
    Rigidbody ArrowRigidbody;
    public GameObject LastBoss;
    public LB_Attack LbAttack;

    // Start is called before the first frame update
    void Start()
    {
        LastBoss = GameObject.Find("LastBoss(Clone)");
        LbAttack = LastBoss.GetComponent<LB_Attack>();
        ArrowRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision Arrow_Collision)//当たり判定処理
    {
        Debug.Log("何かに当たった");                              //デバックログを表示
        if (Arrow_Collision.gameObject.name == "StageBass")
        {
            Destroy(ArrowUp);
            Debug.Log("ArrowUpがワープした");                             //デバックログを表示
            ArrowDisapperCount = 0;                                     //カウントリセット
            Debug.Log("Arrow_Disapper_Countを０にした");                 //デバックログを表示
            LbAttack.ArrowUseFlag = true;
            Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //デバックログ
            LbAttack.ArrowCount++;
            Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //デバックログ
            LbAttack.LBossAnim.SetBool("OnlyFlg", false);
            LbAttack.ArrowNum++;

        }
        if (Arrow_Collision.gameObject.tag== "Ground")        //もし当たったモノにGroundタグが付いていた場合
        {
           Debug.Log("Tag:Groundを持つオブジェクトに当たった");   //デバックログを表示
            
                //Arrow_Rightの場合================================================================================
                if (gameObject.name == "Ult_Right(Clone)")
                {           //接触しているのがgameObjectの"Arrow_Right"の時
                    Destroy(ArrowRight);
                    ArrowDisapperCount = 0;                      //カウントリセット
                    Debug.Log("Arrow_Disapper_Countを０にした");  //デバックログを表示
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag); //デバックログ
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);//デバックログ
                    LbAttack.LBossAnim.SetBool("OnlyFlg", false);
                    LbAttack.ArrowNum++;
                }
                //-------------------------------------------------------------------------------------------------

                //Arrow_Leftの場合==================================================================================
                if (gameObject.name == "Ult_Left(Clone)")
                {          //接触しているのがgameObjectの"Arrow_Left"の場合
                    Destroy(ArrowLeft);
                    Debug.Log("Arrow_Leftがワープした");        //デバックログを表示
                    ArrowDisapperCount = 0;                    //カウントリセット
                    Debug.Log("Arrow_Disapper_Countを０にした");//デバックログを表示
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //デバックログ
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //デバックログ
                    LbAttack.LBossAnim.SetBool("OnlyFlg", false);
                    LbAttack.ArrowNum++;
                }
                //-------------------------------------------------------------------------------------------------
            
        }
    }
}

