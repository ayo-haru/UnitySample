//==========================================================
//      ボスへのダメージ
//      作成日　2022/03/11
//      作成者　藤山凌希
//      
//      <開発履歴>
//      2022/03/11  ダメージ計算
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   
    [SerializeField]  private int ShortRangeAttack = 0; //近距離攻撃のダメージ

    static public int  damage;                            //ダメージ格納用
    

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        //SucRefrect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            damage = ShortRangeAttack;
            Debug.Log("damage : " + damage);
        }

    }
}
