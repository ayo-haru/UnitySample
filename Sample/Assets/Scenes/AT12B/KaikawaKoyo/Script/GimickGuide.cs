using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimickGuide : MonoBehaviour
{
    [SerializeField]
    private bool Guide_Right;
    [SerializeField]
    private bool Guide_RightUp;
    [SerializeField]
    private bool Guide_RightDown;
    [SerializeField]
    private bool Guide_Left;
    [SerializeField]
    private bool Guide_LeftUp;
    [SerializeField]
    private bool Guide_LeftDown;

    private ParticleSystem effect;
    private bool Play;      // エフェクト再生フラグ
    private float EffectTime = 5.1f;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 範囲内にいる間は繰り返し再生する
        if(Play)
        {
            Timer += Time.deltaTime;
            if (EffectTime < Timer)
            {
                effect.Play();
                Timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが近づいたらエフェクトを再生する
        if (other.CompareTag("Player") && !Play)
        {
            // 右
            if (Guide_Right)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT]);
            }
            // 右上
            if (Guide_RightUp)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_UP]);
            }
            // 右下
            if (Guide_RightDown)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_DOWN]);
            }
            // 左
            if (Guide_Left)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT]);
            }
            // 左上
            if (Guide_LeftUp)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_UP]);
            }
            // 左下
            if (Guide_LeftDown)
            {
                effect = Instantiate(EffectData.EF[(int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_DOWN]);
            }
            effect.transform.position = transform.position;
            Play = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // プレイヤーが離れたらエフェクトを停止する
        if (other.CompareTag("Player") && Play)
        {
            Destroy(effect.gameObject, 0.0f);   // エフェクト削除
            Timer = 0.0f;                       // タイマーのリセット
            Play = false;
        }
    }
}
