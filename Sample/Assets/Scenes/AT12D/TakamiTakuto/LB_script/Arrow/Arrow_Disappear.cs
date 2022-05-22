using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Disappear : MonoBehaviour
{
    public GameObject ArrowRight;
    public GameObject ArrowLeft;
    public GameObject ArrowUp;
    public Vector3 ArrowRightinitpos;
    public Vector3 ArrowLeftinitpos;
    public Vector3 ArrowUpinitpos;
    public int ArrowDisapperCount;
    public bool Attackspace=false;
    [SerializeField]  public int Arrow_MaxCount;
    Rigidbody ArrowRigidbody;
    public LB_Attack LbAttack;

    // Start is called before the first frame update
    void Start()
    {
        ArrowRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider Arrow_Collision)//当たり判定処理
    {
        Debug.Log("何かに当たった");                              //デバックログを表示
        if (Arrow_Collision.gameObject.name == "Stage")
        {
            ArrowUp.transform.position = new Vector3(ArrowUpinitpos.x, ArrowUpinitpos.y, ArrowUpinitpos.z);
            ArrowRigidbody.velocity = Vector3.zero;                     //Rigidbodyをストップさせて動きを止める
            Debug.Log("ArrowUpがワープした");                             //デバックログを表示
            ArrowDisapperCount = 0;                                     //カウントリセット
            Debug.Log("Arrow_Disapper_Countを０にした");                 //デバックログを表示
            LbAttack.ArrowUseFlag = true;
            Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //デバックログ
            LbAttack.ArrowCount++;
            Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //デバックログ

        }
        else if (Arrow_Collision.gameObject.tag== "Ground")        //もし当たったモノにGroundタグが付いていた場合
        {
           Debug.Log("Tag:Groundを持つオブジェクトに当たった");   //デバックログを表示
            ArrowDisapperCount++;                           　 //アローを消すカウントを加算
            
            if (ArrowDisapperCount >= Arrow_MaxCount){           //アローカウントがアローマックスと同等、またはそれ以上の時

                //Arrow_Rightの場合================================================================================
                if (gameObject.name == "Arrow_Right"){           //接触しているのがgameObjectの"Arrow_Right"の時
                    ArrowRight.transform.position = new Vector3(ArrowRightinitpos.x, ArrowRightinitpos.y, ArrowRightinitpos.z);
                    ArrowRigidbody.velocity = Vector3.zero;      //Rigidbodyをストップさせて動きを止める
                    Debug.Log("Arrow_Rightがワープした");         //デバックログを表示
                    ArrowDisapperCount = 0;                      //カウントリセット
                    Debug.Log("Arrow_Disapper_Countを０にした");  //デバックログを表示
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag); //デバックログ
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //デバックログ
                }
                //-------------------------------------------------------------------------------------------------

                //Arrow_Leftの場合==================================================================================
                if (gameObject.name == "Arrow_Left"){          //接触しているのがgameObjectの"Arrow_Left"の場合
                    ArrowLeft.transform.position = new Vector3(ArrowLeftinitpos.x, ArrowLeftinitpos.y, ArrowLeftinitpos.z);
                    ArrowRigidbody.velocity = Vector3.zero;    //Rigidbodyをストップさせて動きを止める
                    Debug.Log("Arrow_Leftがワープした");        //デバックログを表示
                    ArrowDisapperCount = 0;                    //カウントリセット
                    Debug.Log("Arrow_Disapper_Countを０にした");//デバックログを表示
                    LbAttack.ArrowUseFlag = true;
                    Debug.Log("Bool:" + LbAttack.ArrowUseFlag);              //デバックログ
                    LbAttack.ArrowCount++;
                    Debug.Log("ArrowCount:" + LbAttack.ArrowCount);             //デバックログ
                }
                //-------------------------------------------------------------------------------------------------
            }
        }
    }
}

