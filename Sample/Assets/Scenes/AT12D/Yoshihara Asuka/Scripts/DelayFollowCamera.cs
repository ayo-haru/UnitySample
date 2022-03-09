//=============================================================================
//
// プレイヤーの追従カメラ(遅延)[DelayFollowCamera]
//
// <Refelence>
// 少し遅れて追従するカメラ設定。ただMainCameraの値で動くのでそれに依存。
// 今後使うかわからないから、一応おいときます。
//
// 作成日:2022/03/08
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/08 作成
//=============================================================================

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
        transform.position = Vector3.Lerp(transform.position,
                                          Player.transform.position + OffSet,
                                          16.0f * Time.deltaTime);
    }
}
