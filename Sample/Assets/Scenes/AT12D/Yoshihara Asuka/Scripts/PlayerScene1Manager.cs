//=============================================================================
//
// シーンのにおけるデータのマネージャ[PlayerScene1Manager]
//
// 作成日:2022/03/11
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/11 作成
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScene1Manager : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("SD_unitychan_humanoid");
        this.Player.transform.position = new Vector3(1.0f,0.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
