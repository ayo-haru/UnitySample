//=============================================================================
//
// テクスチャ回転
//
//
// 作成日:2022/04/24
// 作成者:小楠裕子
//
// <開発履歴>
// 2022/04/24 作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRotate : MonoBehaviour
{
    //画像
    private RectTransform image;
    //回転角度
    private Vector3 rot;
    //回転速度
    public float rotSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<RectTransform>();
        rot = new Vector3(0.0f, 0.0f, rotSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        image.Rotate(rot);
    }
}
