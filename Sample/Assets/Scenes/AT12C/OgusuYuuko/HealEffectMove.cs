//=============================================================================
//
// 回復エフェクト移動用
//
// 作成日:2022/05/30
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/30   作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = GameData.PlayerPos;
    }
}
