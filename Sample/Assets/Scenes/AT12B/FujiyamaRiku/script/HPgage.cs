//==========================================================
//      ボスのゲージ作成
//      作成日　2022/03/10
//      作成者　藤山凌希
//      
//      <開発履歴>
//      2022/03/10  ゲージ実装
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPgage : MonoBehaviour
{
    [SerializeField] int MAXHP = 0;     //最大HP数値変更可
    int currentHp;                      //現在HP
    public Slider slider;               //スライダー
    int m_DelHp ;                      //ダメージ収納用変数
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;               //スライダーの最大値
        currentHp = MAXHP;              //現在のHPを最大HPにする
        m_DelHp = 0;
        Debug.Log("Start currentHp : " + currentHp);
    }

    // Update is called once per frame
    void Update()
    {
        //ダメージを受けたモーション再生後これを実行←だれか任せた
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DelHP();
            Debug.Log("After currentHp : " + currentHp);
        }
        slider.value = (float)currentHp / (float)MAXHP;  //スライダーの長さの計算
    }
    //ダメージ受けた時処理
    private void DelHP()
    {
        m_DelHp = Damage.damage;                //受けたダメージを受け取る
        currentHp = currentHp - m_DelHp;               //現在のHPから受けたダメージ分減らす
        Debug.Log("delHP : " + m_DelHp);
        Debug.Log("m_damage : " + Damage.damage);
    }
}
