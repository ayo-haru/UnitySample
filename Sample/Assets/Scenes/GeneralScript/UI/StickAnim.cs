using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAnim : MonoBehaviour
{
    private UVScroll _uvscroll;   // 自身のUVスクロールを格納 
    private int AnimTimer;        // アニメーションのタイマー
    private int ANIMTIMER = 66 * 2;   // アニメーションのタイマー

    // Start is called before the first frame update
    void Start()
    {
        _uvscroll = this.GetComponent<UVScroll>();  // 自身のUVスクロールを取得
        AnimTimer = ANIMTIMER;
    }

    // Update is called once per frame
    void Update()
    {
        AnimTimer--;
        if(AnimTimer < 0)
        {
            if (_uvscroll.GetnFrame() == 0)
            {
                _uvscroll.SetNext();
            }
            else if(_uvscroll.GetnFrame() == 1)
            {
                _uvscroll.SetPrev();
            }

            AnimTimer = ANIMTIMER;
        }
    }
}
