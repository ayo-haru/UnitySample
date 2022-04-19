using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAnim : MonoBehaviour
{
    private UVScroll _uvscroll;   // 自身のUVスクロールを格納 
    private int AnimTimer;        // アニメーションのタイマー
    private int ANIMTIMER = 66 * 2;   // アニメーションのタイマー

    /*
        フレーム数をカウントする変数。ゲッター作ったらけす。
    */
    private int _nframe;

    // Start is called before the first frame update
    void Start()
    {
        _uvscroll = this.GetComponent<UVScroll>();  // 自身のUVスクロールを取得
        AnimTimer = ANIMTIMER;
        _nframe = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AnimTimer--;
        if(AnimTimer < 0)
        {
            if (_nframe == 0)
            {
                _uvscroll.SetNext();
                _nframe++;
            }else if(_nframe == 1)
            {
                _uvscroll.SetPrev();
                _nframe--;
            }

            AnimTimer = ANIMTIMER;
        }
    }
}
