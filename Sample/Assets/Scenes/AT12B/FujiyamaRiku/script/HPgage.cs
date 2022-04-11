//==========================================================
//      ボスのゲージ作成
//      作成日　2022/03/10
//      作成者　藤山凌希
//      
//      <開発履歴>
//      2022/03/10  ゲージ実装
//      2022/03/15  HPがゼロになったときボスのFlgをfalseに
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPgage : MonoBehaviour
{
    [SerializeField] int MAXHP = 0;     //最大HP数値変更可
    public static int currentHp;        //現在HP
    //public Slider slider;               //スライダー
    //ゲージ用画像
    private Image HpGageImage;
    public static int m_DelHp ;         //ダメージ収納用変数
    public static int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        HpGageImage = gameObject.GetComponent<Image>();
        HpGageImage.fillAmount = 1.0f;
        currentHp = MAXHP;              //現在のHPを最大HPにする
        m_DelHp = 0;
        //Debug.Log("Start currentHp : " + slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        //ダメージを受けたモーション再生後これを実行←だれか任せた
        if(Input.GetKeyDown(KeyCode.F1))
        {
           
                damage = 51;
                DelHP();
            Debug.Log("After currentHp : " + currentHp);
        }//ゲージの長さ
        HpGageImage.fillAmount = (float)currentHp / MAXHP;
        if (currentHp <= 0)
        {
            //ボスのタグ付けして全部のボスで使えるようにしたい所存
            GameData.isAliveBoss1 = false;
        }
    }
    //ダメージ受けた時処理
    public static void DelHP()
    {
        m_DelHp = damage;                //受けたダメージを受け取る
        currentHp = currentHp - m_DelHp;               //現在のHPから受けたダメージ分減らす
        Debug.Log("delHP : " + m_DelHp);
        Debug.Log("m_damage : " + damage);
    }
    
}
