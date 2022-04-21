using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SavePoint : MonoBehaviour
{
    private Camera _targetCamera;   // このシーンで使われてるカメラを取得
    private Canvas _parentUIobj;    // UIの親（今回はキャンバス）
    private GameObject _targetUIobj;    // 目的のUI
    private RectTransform _parentUI;    // 親のUIのレクトトランスフォーム
    private RectTransform _targetUI;    // 目的のUIのレクトトランスフォーム

    private bool isHitPlayer;   // プレイヤーと衝突したか

    private Game_pad UIActionAssets;                // InputActionのUIを扱う
    private InputAction LeftStickSelect;            // InputActionのselectを扱う
    private InputAction RightStickSelect;           // InputActionのselectを扱う


    // Start is called before the first frame update
    void Start()
    {
        isHitPlayer = false;    // 初期化あたっていないのでfalse
        Vector3 Pos = this.transform.position;  // UIの描画の基準値
        EffectManager.Play(EffectData.eEFFECT.EF_MAGICSQUARE, new Vector3(Pos.x, Pos.y - 5.0f, Pos.z),false);   // 表示するUI
        _targetCamera = Camera.main;    // メインカメラを取得
        _parentUIobj = GameObject.Find("Canvas").GetComponent<Canvas>();    // キャンバスを取得
        _parentUI = _parentUIobj.GetComponent<RectTransform>(); // キャンバスのレクトトランスフォーム
        _targetUIobj = _parentUIobj.transform.GetChild(3).gameObject;   // 目的のUIを取得
        _targetUI = _targetUIobj.GetComponent<RectTransform>(); // 目的のUIのレクトトランスフォーム
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitPlayer)    // プレイヤーが当たったら座標を変更
        {
            OnUIPositionUpdate();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            isHitPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            isHitPlayer = false;
        }
    }

    /// <summary>
    /// 呼ばれた時だけUIの位置を変更
    /// </summary>
    void OnUIPositionUpdate() {
        // オブジェクトの位置
        Vector3 targetWorldPos = this.transform.position;
        targetWorldPos.y = targetWorldPos.y + 10.0f;
        // オブジェクトのワールド座標→スクリーン座標変換
        Vector3 targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out Vector2 uiLocalPos
        );

        // RectTransformのローカル座標を更新
        _targetUI.localPosition = uiLocalPos;

    }
}
