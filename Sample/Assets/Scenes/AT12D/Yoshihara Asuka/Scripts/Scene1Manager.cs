//=============================================================================
//
// �e�V�[���̃f�[�^�Ǘ�[Scene1Manager]
//
// �쐬��:2022/03/11
// �쐬��:�g����
//
// <�J������>
// 2022/03/11
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    GameObject Player;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
