//=============================================================================
//
// 各シーンのデータ管理[Scene1Manager]
//
// 作成日:2022/03/11
// 作成者:吉原飛鳥
//
// <開発履歴>
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
