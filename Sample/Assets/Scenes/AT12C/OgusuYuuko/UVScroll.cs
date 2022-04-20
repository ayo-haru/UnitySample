//=============================================================================
//
// テクスチャのUV制御
//
// 作成日:2022/04/04
// 作成者:小楠裕子
//
// ＵＩ用に作成
//
// 表示画像にこのスクリプト入れる
// マスクコンポーネントがアタッチされた画像を親に指定して、ShowMaskGraphicのチェック外す
// 親の位置が画像の表示位置になる
//
// <開発履歴>
// 2022/04/04 作成
// 2022/04/06 次に進む、前に戻るを追加
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVScroll : MonoBehaviour
{
    //コンポーネント取得
    RectTransform rt;
    //親 マスク画像
    GameObject pearent;
    //親コンポーネント
    RectTransform pearent_rt;
    [SerializeField]
    //表示する枠
    private int nFrame = 0;
    [SerializeField]
    //テクスチャ分割数横
    private int split_x = 1;
    [SerializeField]
    //テクスチャ分割数縦
    private int split_y = 1;

    ///1枠の大きさ
    private float width;
    private float height;

    private void Awake()
    {
        //自分のRectTransform取得
        rt = GetComponent<RectTransform>();
        //親オブジェクト取得
        pearent = transform.parent.gameObject;
        //親のRectTransform取得
        pearent_rt = pearent.GetComponent<RectTransform>();
        //表示する枠の大きさ
        width = rt.sizeDelta.x / split_x;
        height = rt.sizeDelta.y / split_y;
        //枠を親のマスクに設定
        pearent_rt.sizeDelta = new Vector2(width, height);
        //テクスチャの位置設定
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                     pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (int)(nFrame / split_x) * height,
                                      0.0f);
    }

    private void Update()
    {
        
    }

    public void SetFrame(int FrameNo)
    {
        nFrame = FrameNo;

        //nFrame補正　0～テクスチャの分割数 - 1の間にする
        if(nFrame >= split_x * split_y)
        {
            nFrame = FrameNo % (split_x * split_y);
        }else if (nFrame < 0)
        {
            FrameNo *= -1;
            nFrame = (split_x * split_y) - (FrameNo % (split_x * split_y));
            if (nFrame == split_x * split_y)
            {
                nFrame = 0;
            }
        }

        //テクスチャの位置設定
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                      pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (nFrame / split_x) / split_y * height,
                                       0.0f);
    }

    public void SetNext()
    {
        ++nFrame;
        if(nFrame >= split_x * split_y)
        {
            nFrame = 0;
        }
        //テクスチャの位置設定
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                      pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (nFrame / split_x) / split_y * height,
                                       0.0f);
    }

    public void SetPrev()
    {
        --nFrame;
        if(nFrame < 0)
        {
            nFrame = split_x * split_y - 1;
        }
        //テクスチャの位置設定
        rt.position = new Vector3(pearent.transform.position.x + (rt.sizeDelta.x / 2) - (width / 2) - ((nFrame % split_x) * width),
                                      pearent.transform.position.y - (rt.sizeDelta.y / 2) + (height / 2) + (nFrame / split_x) / split_y * height,
                                       0.0f);
    }

    /*
     *
     *  nFrameの取得
     * 
     */
    public int GetnFrame() {
        return nFrame;
    }
}
