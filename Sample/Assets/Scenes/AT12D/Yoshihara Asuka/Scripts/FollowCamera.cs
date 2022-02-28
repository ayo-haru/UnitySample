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
        Vector3 PlayerPos = this.Player.transform.position;
        //transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 1.0f, PlayerPos.z - 2.0f);
        transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 1.0f, -2.0f);
    }
}
