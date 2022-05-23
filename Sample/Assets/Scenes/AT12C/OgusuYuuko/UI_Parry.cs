//=============================================================================
//
// UIƒpƒŠƒC
//
// ì¬“ú:2022/05/20
// ì¬Ò:¬“í—Tq
//
// <ŠJ”­—š—ğ>
// 2022/05/20    ì¬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parry : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //’e‚©‚ê‚½‚Ì‘¬‚³
    public float ParrySpeed = 10.0f;
    //’e‚­•ûŒü
    public Vector2 ParryDir = new Vector2(0.0f, -1.0f);
    //ˆê‰ñ‚¾‚¯ˆ—‚·‚é‚æ‚¤
    private bool onceFlag;
    //‰ŠúˆÊ’u
    private Vector3 startPos;
    //’e‚©‚ê‚½‚©
    public bool underParryFlag;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RectTransform>();
        startPos = image.position;
        onceFlag = true;
        underParryFlag = false;
    }

    private void OnDisable()
    {
        onceFlag = true;
        underParryFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onceFlag)
        {
            image.position = startPos;
            onceFlag = false;
        }

        if (underParryFlag)
        {
            image.transform.position += image.transform.up * (ParrySpeed * ParryDir.y);
            image.transform.position += image.transform.right * (ParrySpeed * ParryDir.x);
        }
    }
}
