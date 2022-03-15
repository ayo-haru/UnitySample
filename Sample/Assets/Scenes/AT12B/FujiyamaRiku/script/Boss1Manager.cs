//==========================================================
//      ボスのゲージ作成
//      作成日　2022/03/14
//      作成者　藤山凌希
//      
//      <開発履歴>
//      2022/03/14      ボスマネージャーの作成  
//
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour
{
    public enum Boss1State
    { 
        BOSS1_START = 0,    //ボスの開始
        BOSS1_BATTLE ,      //ボスとのバトル中
        BOSS1_END,          //ボスを倒したとき
    }
    private void Awake()
    {
        //ボスを倒したかどうかの判定
        if (!GameData.isAliveBoss1)
        {
            //ボスの処理を何もしない処理入れる
            return;
        }
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
