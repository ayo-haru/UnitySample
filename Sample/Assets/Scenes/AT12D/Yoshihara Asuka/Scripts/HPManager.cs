//=============================================================================
//
// HP�Ǘ�
//
// �쐬��:2022/03/25
// �쐬��:�g����
//
// <�J������>
// 2022/03/25 �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    private void Awake()
    {
        //---�J�n���ɕ\������UI(GameObject)�̃A�N�e�B�u��Ԃ�ݒ� true = �L�� / false = ����
        GameObject.Find("Full Moon").SetActive(true);
        GameObject.Find("Harf Moon1").SetActive(false);
        GameObject.Find("Harf Moon2").SetActive(false);
        GameObject.Find("Harf Moon3").SetActive(false);
        GameObject.Find("New Moon").SetActive(false);
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
