//=============================================================================
//
// Playerの共通変数を設定
//
// 作成日:2022/03/18
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/04/18   作成
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    //---共通の変数一覧
    public static Game_pad PlayerActionAssets;              // InputActionで使用するものGamePadの設定
    public static Animator animator;                        // アニメーター

    public static bool flg;
}
