using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayFollowCamera : MonoBehaviour
{
    //---変数宣言
    private GameObject Player;
    private Vector3 OffSet;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("SD_unitychan_humanoid");          // 追従するオブジェクトを設定
        OffSet = transform.position - Player.transform.position;    // プレイヤーの座標分少しオフセットを下げる

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(Player.transform.position,
                                          Player.transform.position + OffSet,
                                          16.0f * Time.deltaTime);
    }
}
