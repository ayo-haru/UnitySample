using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    /// <summary>
    /// エフェクトの再生
    /// </summary>
    /// <param name="_effectDataNumber">EffectDataで定義したやつ</param>
    /// <param name="_pos">表示したい座標。オブジェクトの座標のままだと中心点に出るので注意。</param>
    /// <param name="_destroy">消したくない場合のみfalseで指定</param>
    public static void Play(EffectData.eEFFECT _effectDataNumber,Vector3 _pos,bool _destroy = true) {
        if (!EffectData.isSetEffect)
        {
            return; // エフェクトデータ未設定
        }
        //エフェクトを生成する
        ParticleSystem effect = Instantiate(EffectData.EF[(int)_effectDataNumber]);
        //エフェクトが発生する場所を決定する(敵オブジェクトの場所)
        effect.transform.position = _pos;
        effect.Play();
        if (_destroy)
        {
            Destroy(effect.gameObject, 5.0f);
        }
    }

    /// <summary>
    /// エフェクトの再生
    /// </summary>
    /// <param name="_effectDataNumber">EffectDataで定義したやつ</param>
    /// <param name="_pos">表示したい座標。オブジェクトの座標のままだと中心点に出るので注意。</param>
    /// <param name="_destroyTime">消える秒数指定</param>
    public static void Play(EffectData.eEFFECT _effectDataNumber, Vector3 _pos,float _destroyTime) {
        if (!EffectData.isSetEffect)
        {  
            return; // エフェクトデータ未設定
        }
        //エフェクトを生成する
        ParticleSystem effect = Instantiate(EffectData.EF[(int)_effectDataNumber]);
        //エフェクトが発生する場所を決定する(敵オブジェクトの場所)
        effect.transform.position = new Vector3(_pos.x,_pos.y,-5);

        effect.Play();
        // 生存時間を設定したものにする
        Destroy(effect.gameObject, _destroyTime);
    }

    /// <summary>
    /// エフェクトポーズ
    /// </summary>
    public static void EffectPause() {
        if (!EffectData.isSetEffect)    // エフェクトデータ未設定ならやらない
        {
            return;
        }

        if (EffectData.oncePauseEffect) // ポーズに入ったら一回だけ実行（オブジェクトを探す処理が重いため））
        {
            SetAllActiveEffect();   // 重たいから絶対に毎フレームやらない
            EffectData.oncePauseEffect = false; // 一度だけやるためにフラグを下す
        }

        for (int i = 0; i < EffectData.activeEffect.Length; i++) {  
            if (EffectData.activeEffect[i] != null)// エフェクトの数だけポーズする
            {
                EffectData.activeEffect[i].GetComponent<ParticleSystem>().playbackSpeed = 0.0f;
                //EffectData.activeEffect[i].playbackSpeed = 0.0f;
            }
        }
    }

    /// <summary>
    /// エフェクトのポーズ解除
    /// </summary>
    public static void EffectUnPause() {
        if (!EffectData.isSetEffect)    // エフェクトデータ未設定ならやらない
        {
            return;
        }

        for (int i = 0; i < EffectData.activeEffect.Length; i++)
        {
            if (EffectData.activeEffect[i] != null)// エフェクトの数だけポーズ解除
            {
                EffectData.activeEffect[i].GetComponent<ParticleSystem>().playbackSpeed = 1.0f;
                //EffectData.activeEffect[i].playbackSpeed = 1.0f;
                EffectData.activeEffect[i] = null;
            }
        }

        EffectData.oncePauseEffect = true;
    }

    /// <summary>
    /// 現在使われているエフェクトをすべて探す
    /// </summary>
    private static void SetAllActiveEffect() {
       int index = 0;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))    // そのシーンのすべてのオブジェクトをだす
        {
            if (obj.GetComponent<ParticleSystem>()) // 出したオブジェクトがパーティクルシステムだったらエフェクトのデータに渡す
            {
                EffectData.activeEffect[index] = obj;
                //Debug.Log(EffectData.activeEffect[index]);
                //Debug.Log(index);
                index++;
            }
        }

        //foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        //{
        //    if (obj.GetComponent<ParticleSystem>())
        //    {
        //        if (!obj.transform.parent)
        //        {
        //            EffectData.activeEffect[index] = obj.GetComponent<ParticleSystem>();
        //            Debug.Log(EffectData.activeEffect[index]);
        //            Debug.Log(index);
        //            index++;
        //        }
        //    }
        //}

    }
}
