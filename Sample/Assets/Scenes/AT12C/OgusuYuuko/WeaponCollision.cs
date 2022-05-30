//=============================================================================
//
// 反射板当たり判定
//
//
// 作成日:2022/03/27
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/27 作成
// 2022/03/29 盾の個数制限を付けた
//=============================================================================

//コメント追加
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    //プレイヤー
    GameObject _Player;
    //プレイヤーのリジットボディ
    Rigidbody player_rb;
    //地面パリイした時のはね返り速度
    public float baunceGround = 2.0f;
    //壁キックした時のはね返り強さ
    public Vector2 baunceWall = new Vector2(10.0f, 1.0f);
    //壁キックできる左右パリイの範囲
    public float baunceWallArea = 0.1f;
    //シールドマネージャ
    ShieldManager shield_Manager;

    Player2 player2;
    
    private bool CanCollision = true;                      // 当たり判定の使用フラグ

    // Start is called before the first frame update
    void Awake()
    {
        //Player = GameData.Player;
        this._Player = GameObject.Find(GameData.Player.name);
        player_rb = this._Player.GetComponent<Rigidbody>();
        shield_Manager = this._Player.GetComponent<ShieldManager>();
        player2 = this._Player.GetComponent<Player2>();


        //盾が最大数を超えていたら
        if (!shield_Manager.AddShield())
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        shield_Manager.DestroyShield();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector2 dir = (this._Player.transform.position - this.gameObject.transform.position).normalized;
        if (CanCollision)
        {
            if (collision.gameObject.tag == "Ground" || 
                collision.gameObject.tag == "GroundDameged" || 
                collision.gameObject.tag == "LastBossWeapon")
            {
                if (Player.isHitSavePoint)
                {
                    CanCollision = false;
                    return;
                }

                BounceCheck();
                //方向
                //Vector3 dir = Player.transform.position - collision.transform.position;

                
                //Vector2 dir = this.gameObject.transform.position - Player.transform.position;
                //Debug.Log("距離測定" + dir);

                //dir.Normalize();
                Debug.Log("距離測定(正規化)" + dir);


                player2.rb.velocity = Vector3.zero;

                
                //地面パリイ
                //player_rb.AddForce(dir, ForceMode.Impulse);
                player_rb.AddForce(dir * baunceGround, ForceMode.Impulse);
               //player2.SetJumpPower(dir * baunceGround);

                CanCollision = false;
                
            }

            //壁キック用の壁と当たってて、左右に盾が出されていた場合は壁キックする
            if (collision.gameObject.tag == "WallBlock" && (dir.x > baunceWallArea || dir.x < -baunceWallArea))
            {

                Debug.Log("壁キック");
                if (dir.x > 0)//右上
                {
                    dir.x = baunceWall.x;
                    dir.y = baunceWall.y;

                }
                else if (dir.x < 0)//左上
                {
                    dir.x = -baunceWall.x;
                    dir.y = baunceWall.y;

                }
                BounceCheck();
                player2.rb.velocity = Vector3.zero;
                player_rb.AddForce(dir * baunceGround, ForceMode.Impulse);
                CanCollision = false;
            }

            if (collision.gameObject.tag == "Enemy"){
                player2.OnAttackHit();
                StartCoroutine(player2.VibrationPlay(0.2f, 0.2f, 0.05f));
                Debug.Log("攻撃振動");
                EffectManager.Play(EffectData.eEFFECT.EF_PLAYER_BREAK, transform.position);
            }
            else
            {
                player2.CanHitStopflg = false;
                //StopCoroutine(player2.VibrationPlay(1.0f, 1.0f, 0.25f));
            }

            if(collision.gameObject.name == "TutorialFork(Clone)")
            {
                TutorialPanCake.RefrectFlg = true;
            }
        }

        //プレイヤー以外と当たってたら盾消去
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject,0.1f);
        }

        Debug.Log(collision.gameObject.name + "と当たった");
    }

    // 跳ね返る力調整関数
    private void BounceCheck()
    {
        Vector3 PlayerVelocity = player_rb.velocity;
        PlayerVelocity.z = 0;

        if(PlayerVelocity.sqrMagnitude > baunceGround * baunceGround)
        {
            player_rb.velocity = PlayerVelocity.normalized * baunceGround;
        }
    }

private void PlayBreakEffect()
	{
        EffectManager.Play(EffectData.eEFFECT.EF_PLAYER_BREAK, transform.position);
	}
}
