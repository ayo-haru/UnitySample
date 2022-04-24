//=============================================================================
//
// �e�N�X�`����]
//
//
// �쐬��:2022/04/24
// �쐬��:����T�q
//
// <�J������>
// 2022/04/24 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRotate : MonoBehaviour
{
    //�摜
    private RectTransform image;
    //��]�p�x
    private Vector3 rot;
    //��]���x
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
