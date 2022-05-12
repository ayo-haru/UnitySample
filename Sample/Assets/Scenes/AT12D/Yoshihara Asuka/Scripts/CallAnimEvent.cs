using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーションイベント処理
/// </summary>
public class CallAnimEvent : MonoBehaviour
{
    //---プレイヤーのついてるコンポーネント取得
    GameObject player;
    Player2 player2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(GameData.Player.name);
        player2 = player.GetComponent<Player2>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 登録したOnCreateShiled()で行う処理を記載
    /// </summary>
    //   public void OnCreateShiled()
    //{
    //       player2.Attack();
    //       Debug.Log("アニメーションイベントのほう");
    //}

    //   /// <summary>
    //   /// アニメーション開始時にアタックフラグを立てる
    //   /// </summary>
    //   public void StartAnim()
    //{
    //       player2.SetAttackFlg(true);
    //}

    /// <summary>
    /// アニメーション終了時にアタック許可フラグおろす
    /// </summary>
    public void SideShield()
    {
        player2.CreateShiled();
    }

    public void UnderShiled()
    {
        player2.CreateShiled();
    }

    public void OverShield()
    {
        player2.CreateShiled();
    }


}
