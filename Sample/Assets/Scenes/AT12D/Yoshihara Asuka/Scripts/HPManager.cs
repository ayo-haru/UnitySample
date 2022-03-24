//=============================================================================
//
// HP管理
//
// 作成日:2022/03/25
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/25 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    private void Awake()
    {
        //---開始時に表示するUI(GameObject)のアクティブ状態を設定 true = 有効 / false = 無効
        GameObject.Find("Full Moon").SetActive(true);
        GameObject.Find("Harf Moon1").SetActive(false);
        GameObject.Find("Harf Moon2").SetActive(false);
        GameObject.Find("Harf Moon3").SetActive(false);
        GameObject.Find("New Moon").SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
