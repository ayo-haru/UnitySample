//=============================================================================
//
// �񕜃G�t�F�N�g�ړ��p
//
// �쐬��:2022/05/30
// �쐬��:����T�q
//
// <�J������>
// 2022/05/30   �쐬
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffectMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = GameData.PlayerPos;
    }
}
