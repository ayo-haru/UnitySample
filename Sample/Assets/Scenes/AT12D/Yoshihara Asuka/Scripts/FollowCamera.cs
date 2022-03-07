using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowCamera : MonoBehaviour
{
    GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        // 追従するオブジェクト名を設定
        this.Player = GameObject.Find("SD_unitychan_humanoid");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーに追従する
        Vector3 PlayerPos = this.Player.transform.position;

        // *****座標*****
        transform.position = new Vector3(PlayerPos.x,0.7f, PlayerPos.z - 5.0f);
        //transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 0.7f, -4.0f);     // ジャンプ追従

        // *****回転*****
        //transform.Rotate = new Vector3(5.0f, 0.0f, 0.0f);

        // 画面外設定(x = 45.0fの地点に到達したらカメラの移動を停止)
        if(PlayerPos.x > 45.0f){
            transform.position = new Vector3(45.0f, 0.7f, PlayerPos.z - 5.0f);
            //transform.position = new Vector3(45.0f, PlayerPos.y + 0.7f, -6.0f);         // ジャンプ追従   
        }
        // 画面外設定(x = 45.0fの地点に到達したらカメラの移動を停止)
        else if (PlayerPos.x < 5.0f){
            transform.position = new Vector3(5.0f, 0.7f, PlayerPos.z - 5.0f);
            //transform.position = new Vector3(5.0f, PlayerPos.y + 0.7f, -6.0f);          // ジャンプ追従
        }
    }
}
