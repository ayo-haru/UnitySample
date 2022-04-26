//=============================================================================
//
// �ݒ��ʕ\��
//
// �쐬��:2022/04/26
// �쐬��:����T�q
//
// <�J������>
// 2022/04/26    �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionOpen : MonoBehaviour
{
    public GameObject Option;

    // Start is called before the first frame update
    void Awake()
    {
        //---MAP�\��
        GameObject canvas = GameObject.Find("Canvas");
        Option = Instantiate(Option);
        Option.transform.SetParent(canvas.transform, false);
    }

    private void Start()
    {
        //���߂͔�\��
        Option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //f3�ŕ\��
        if (Input.GetKey(KeyCode.F3))
        {
            Option.SetActive(true);
        }
        //f�S�Ŕ�\��
        if (Input.GetKey(KeyCode.F4))
        {
            Option.SetActive(false);
        }
    }
}
