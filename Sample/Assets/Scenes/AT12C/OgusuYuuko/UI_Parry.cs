//=============================================================================
//
// UI�p���C
//
// �쐬��:2022/05/20
// �쐬��:����T�q
//
// <�J������>
// 2022/05/20    �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Parry : MonoBehaviour
{
    //RectTransform
    private RectTransform image;
    //�e���ꂽ���̑���
    public float ParrySpeed = 10.0f;
    //��񂾂���������悤
    private bool onceFlag;
    //�����ʒu
    private Vector3 startPos;
    //�e���ꂽ��
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

        if(underParryFlag)
        image.transform.position += image.transform.up * -ParrySpeed;
    }
}
