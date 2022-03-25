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
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public Slider ゲージA;

    private void Awake()
    {
        //---開始時に表示するUI(GameObject)のアクティブ状態を設定 true = 有効 / false = 無効
        //GameObject.Find("Full Moon").SetActive(true);
        //GameObject.Find("Harf Moon1").SetActive(false);
        //GameObject.Find("Harf Moon2").SetActive(false);
        //GameObject.Find("Harf Moon3").SetActive(false);
        //GameObject.Find("New Moon").SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        ゲージA.value = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
