//=============================================================================
//
// スターを管理するクラス
//
// 作成日:2022/05/07
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/05/07 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    //シーンにあるスター
    private GameObject[] starList;

    // Start is called before the first frame update
    void Start()
    {
        //シーンにおいてあるスターを取得
        starList = GameObject.FindGameObjectsWithTag("Item");
        //オブジェクトが取得される順番が分からないため、ｘ座標の位置で並び替える
        for (int i = 0; i < starList.Length - 1; ++i)
        {
            for (int j = i + 1; j < starList.Length; ++j)
            {
                if (starList[i].transform.position.x > starList[j].transform.position.x)
                {
                    GameObject work = starList[i];
                    starList[i] = starList[j];
                    starList[j] = work;
                }else if(starList[i].transform.position.x == starList[j].transform.position.x)
                {//x座標が同じだった場合、y座標の位置で並び替える
                    if(starList[i].transform.position.y > starList[j].transform.position.y)
                    {
                        GameObject work = starList[i];
                        starList[i] = starList[j];
                        starList[j] = work;
                    }
                }
            }
        }

        for (int i = 0; i < starList.Length; ++i)
        {
            //trueだったら取得済みなので消す
            if(GameData.isStarGet[GameData.CurrentMapNumber - 1, i])
            {
                Destroy(starList[i]);
                continue;
            }
            //スターに識別用id割り当て
            starList[i].GetComponent<Star>().SetID(i);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
