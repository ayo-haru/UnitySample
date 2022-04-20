using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //----------------------------------
    //
    //  エフェクト再生
    //  作成：伊地田真衣
    //  詳細：第一引数はEffectDataの列挙体に定義したやーつ
    //
    //----------------------------------
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

    public static void EffectPause() {
        for(int i = 0;i < (int)EffectData.eEFFECT.MAX_EF; i++)
        {
            //if (EffectData.EF[i].isPlaying)
            //{
                //EffectData.EF[i].Pause();
            //}
        }
    }
}
