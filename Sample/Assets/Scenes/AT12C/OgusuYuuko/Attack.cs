//=============================================================================
//
// 攻撃
//
// 作成日:2022/03
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03    作成
// 2022/03/12 モデルの向きに関係なく入力方向に盾が出るように変更
// 2022/03/16 反射板がスライドするようにした
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //シールド位置上下
    public float AttckPosHeight = 2.0f;
    //シールド位置左右
    public float AttckPosWidth = 2.0f;
    //シールド有効時間
    public float DestroyTime = 0.1f;

    //コピー元のオブジェクト
    GameObject prefab;

    //プレイヤーの座標
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの座標取得
        pos = transform.position;

        //上攻撃
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x,pos.y + AttckPosHeight, pos.z), Quaternion.identity);
            weapon.transform.Rotate(new Vector3(0, 0, 90));
            Destroy(weapon, DestroyTime);
            return;
        }
        //下攻撃
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x, pos.y - AttckPosHeight, pos.z), Quaternion.identity);
            weapon.transform.Rotate(new Vector3(0, 0, 90));
           Destroy(weapon, DestroyTime);
            return;
        }
        //左攻撃
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x - AttckPosWidth, pos.y, pos.z), Quaternion.identity);
           Destroy(weapon, DestroyTime);
            return;
        }

        //右攻撃
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject weapon = Instantiate(prefab, new Vector3(pos.x + AttckPosWidth, pos.y, pos.z), Quaternion.identity);
            Destroy(weapon, DestroyTime);
            return;
        }
    }
}
