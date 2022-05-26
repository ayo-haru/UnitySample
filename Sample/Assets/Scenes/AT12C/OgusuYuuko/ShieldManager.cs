//=============================================================================
//
// シールドを管理するクラス
//
// 作成日:2022/03/39
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/03/29   作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public int now_Quantity = 0;   //現在の盾の個数
    public int max_Quantity = 1;   //盾の最大数

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddShield()
    {
        if (Player.isHitSavePoint)
        {
            return false;
        }
        //盾の数更新
        ++now_Quantity;
        Debug.Log("盾出した" + now_Quantity);
        //最大数を超えていたら
        if(now_Quantity > max_Quantity)
        {
            return false;
        }
        return true;

    }

    public void DestroyShield()
    {
        //盾の数更新
        --now_Quantity;
        Debug.Log("盾消した" + now_Quantity);
    }

}
