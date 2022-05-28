//=============================================================================
//
// てくすちゃあにめーしょん
//
// 作成日:2022/05/2
// 作成者:小楠裕子
//
//  UVScroollが入ってる画像に入れる
//
// <開発履歴>
// 2022/05/2    作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
    //表示フレーム数
    public int frame = 3;
    //カウント
    private int count;
    //開始枠
    public int startFrame = 0;
    //終了枠
    public int finishFrame = 0;
    //UVScrool
    private UVScroll uvScroll;
    //ループ有無
    public bool loop = false;
    // Start is called before the first frame update
    void Start()
    {
        //ボスシーンはCanvas2に表示
        GameObject canvas = GameObject.Find("Canvas2");
        if (!canvas)
        {
            //Canvas2が無かったら（ボスシーン以外は）Canvasに表示
            canvas = GameObject.Find("Canvas");
        }

        transform.parent.gameObject.transform.SetParent(canvas.transform, true);

        uvScroll = gameObject.GetComponent<UVScroll>();
        //開始位置にテクスチャセット
        uvScroll.SetFrame(startFrame);
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //カウント更新
        ++count;
        //次のアニメーションへ
        if(count >= frame)
        {
            count = 0;
            uvScroll.SetNext();
            //テクスチャの一部分でループする場合、終了枠を超えていたら開始位置に戻す
            if(loop && uvScroll.GetnFrame() > finishFrame)
            {
                uvScroll.SetFrame(startFrame);
            }
            //テクスチャの一部分でループする場合、開始枠が0じゃない時は開始位置設定をする
            if(loop && startFrame > 0 && uvScroll.GetnFrame() <= 0){
                uvScroll.SetFrame(startFrame);
            }
            //アニメーションが一通り終わって、ループしない場合は消す
            if (!loop && uvScroll.GetnFrame() <= 0)
            {
                Destroy(transform.parent.gameObject);  
            }
        }


    }
}
