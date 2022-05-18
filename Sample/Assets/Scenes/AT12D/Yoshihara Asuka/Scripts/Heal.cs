//=============================================================================
//
// 回復アイテム処理
//
// 作成日:2022/04/16
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/04/16   作成
// 2022/05/07   無限回復直した
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    //---変数宣言
    public GameObject prefab;
    HPManager hpmanager;
    GameObject HP;
    GameObject Player;
    GameObject Star_Fragment;
    Rigidbody rb;
    public float BounceSpeed = 10.0f;                   // 弾かれるスピード
    public float BounceVectorMultiple = 2.0f;           // 法線ベクトルに乗算する値
    public float BouncePower = 10000.0f;                  // 弾かれる値
    private Vector3 vec;                                // 弾くベクトル
    [System.NonSerialized]bool isGroundFlg = false;     // 地面との接地フラグ
    private float aTime;
    private bool isBaunceFlg;                           //弾かれたかの判定

    private Vector3 CamRightTop;    // カメラの右上座標
    private Vector3 CamLeftBot;     // カメラの左下座標
    private Vector3 InAngle;        // 入射角
    private Vector3 ReAngle;        // 反射角
    private Vector3 inNormalU;      // 法線ベクトル上
    private Vector3 inNormalD;      // 法線ベクトル下
    private Vector3 inNormalR;      // 法線ベクトル右
    private Vector3 inNormalL;      // 法線ベクトル左
    private float CamZ = -120.0f;
    private bool Reflect;                               //画面端で弾かれたかの判定

    // Start is called before the first frame update
    void Start()
    {
        // プレハブを複製
        //GameObject HealItem = Instantiate(prefab,
        //                                  new Vector3(0.0f,0.0f,0.0f),
        //                                  Quaternion.identity);
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player");

        HP = GameObject.Find("HPSystem(2)(Clone)");
        hpmanager = HP.GetComponent<HPManager>();

        isBaunceFlg = false;    //まだ弾かれてない

        // 法線ベクトル定義
        inNormalU = new Vector3(0.0f, 1.0f, 0.0f);
        inNormalD = new Vector3(0.0f, -1.0f, 0.0f);
        inNormalR = new Vector3(1.0f, 0.0f, 0.0f);
        inNormalL = new Vector3(-1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        //-----弾かれていた時にやる処理
        if (isBaunceFlg)
        {
            rb.Resume(gameObject);
            // カメラの端の座標取得
            CamRightTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, CamZ));
            CamLeftBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, CamZ));

            // 画面端で跳ね返したい
            // 右端
            if (transform.position.x >= CamRightTop.x && !Reflect)
            {
                InAngle = rb.velocity;
                ReAngle = Vector3.Reflect(InAngle, inNormalL);
                rb.velocity = ReAngle;
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }
            // 左端
            if (transform.position.x <= CamLeftBot.x && !Reflect)
            {
                InAngle = rb.velocity;
                rb.velocity = Vector3.Reflect(InAngle, inNormalR);
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }
            // 上端
            if (transform.position.y >= CamRightTop.y && !Reflect)
            {
                InAngle = rb.velocity;
                rb.velocity = Vector3.Reflect(InAngle, inNormalD);
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }
            // 下端
            if (transform.position.y <= CamLeftBot.y && !Reflect)
            {
                InAngle = rb.velocity;
                rb.velocity = Vector3.Reflect(InAngle, inNormalU);
                Reflect = true;
            }
            else
            {
                Reflect = false;
            }

            return;
        }
        
        //-----弾かれてなかったときの処理
        // プレイヤーとオブジェクトの二点間のベクトルを求める
        vec = (Player.transform.position - transform.position).normalized;
        
        // 地面についたらちょっと浮いてから空中に留まる
        if (isGroundFlg)
        {
            aTime += Time.deltaTime;
            if (aTime < 1.0f)
            {
                rb.AddForce(transform.up * (10.0f * aTime), ForceMode.Force);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            rb.useGravity = false;
            isGroundFlg = true;
            
        }

        if (collision.gameObject.name == "Weapon(Clone)")
        {

            //// 衝突した面の接地点のベクトルを取得
            //Vector3 normal = collision.contacts[0].normal; 
            
            //// 衝突した速度ベクトルを単位ベクトルに置き換える
            //Velocity = collision.rigidbody.velocity.normalized;

            //// x,y,z方向に対して法線ベクトルを取得
            //Velocity += new Vector3(normal.x * BounceVectorMultiple,
            //                        normal.y * BounceVectorMultiple,
            //                        normal.z * BounceVectorMultiple);

            // 逆方向に跳ね返す
            //collision.rigidbody.AddForce(-Velocity * BounceSpeed,ForceMode.Impulse);
            
            rb.useGravity = false;                          // 重力を消す
            rb.angularDrag = 0.0f;                          // 空気抵抗をゼロに
            rb.centerOfMass = new Vector3(0.0f,0.0f,0.0f);  // 回転軸を中央にする

            //プレイヤーと逆方向に跳ね返す
            rb.AddForce(-vec * BouncePower, ForceMode.Force);

            // 回復エフェクトだす
            EffectManager.Play(EffectData.eEFFECT.EF_HEAL, GameData.PlayerPos);

            //既に弾かれてたら処理しない
            if (isBaunceFlg)
            {
                return;
            }
            Destroy(prefab, 1.5f);
            hpmanager.GetPiece();

            //弾かれた
            isBaunceFlg = true;
        }


    }
}
