//=============================================================================
//
// HP管理
//
// 作成日:2022/03/25
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/25 作成
// 2022/03/28 仮実装
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public　Image HP;

    public int MaxHP = 6;                          // HPの最大値

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
        HP.fillAmount = (float)GameData.CurrentHP / MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        HP.fillAmount = (float)GameData.CurrentHP / MaxHP;
        Debug.Log("HP量:"+HP.fillAmount);
        Debug.Log("MAXHP:"+MaxHP);
        Debug.Log("残HP:"+GameData.CurrentHP);
    }
}
