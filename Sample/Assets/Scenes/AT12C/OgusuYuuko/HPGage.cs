//=============================================================================
//
// HPゲージ
//
// 作成日:2022/04/10
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/10 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGage : MonoBehaviour
{
    //ゲージ用画像
    private Image HpGageImage;
    //最大HP
    public int MaxHP = 100;
    //現在のHP
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        HpGageImage = gameObject.GetComponent<Image>();
        HpGageImage.fillAmount = 1.0f;
        currentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲージの長さ
        HpGageImage.fillAmount = (float)currentHP / MaxHP;
    }

    //HPが増減した時にこれを呼ぶ
    //引数 : 現在のHP
    public void HpGageDel(int nHP)
    {
        currentHP = nHP;
    }
}
