using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    Boss1Rain BossRain;
    float timer = 10.0f;                    // スタートの時間

    [SerializeField] private MeshRenderer _meshRenderer;        // 対象のメッシュレンダーを格納
    [SerializeField] private Material _alphaMaterial;           // 変更するマテリアルを格納(今回はα値を変えるマテリアル)

    public float DestoyTime = 0.5f;                             // 消えるまでの時間

    private Material[] _originalMaterials;                      // オブジェクトのもともと持っているマテリアルを格納
    private Material[] _dynamicmaterials;                       // 変更するマテリアルを格納

    private Coroutine _playCoroutine;                           // スタートするコルーチンを格納
    private bool isCalledOnee = false;                          // 一回しか処理しないフラグ


    // Start is called before the first frame update
    void Start()
    {
        
        BossRain = GameObject.Find("PanCake(Clone)").GetComponent<Boss1Rain>();
        
            //---もともと持っているマテリアルを格納
            _originalMaterials = _meshRenderer.materials;

        //---変更するマテリアルの初期化([_meshRenderer.materials.Length]は要素の配列になる)
        _dynamicmaterials = new Material[_meshRenderer.materials.Length];

        //---メッシュレンダーの要素の配列分初期化する
        for(var materialIndex = 0;materialIndex < _meshRenderer.materials.Length; ++materialIndex)
        {
            //---配列の数だけ変更する_dynamicmaterialsに新しいマテリアルに差し替える
            _dynamicmaterials[materialIndex] = new Material(_alphaMaterial);

            //---テクスチャのプロパティをコピー
            _dynamicmaterials[materialIndex].SetTexture("_MainTex",_meshRenderer.sharedMaterials[materialIndex].GetTexture("_MainTex"));

        }
    }

    private void Update()
    {
        
    }

    public void Play()
    {
        //---再生中であれば止める
        Stop();

        //---マテリアルを変更先に差し替える
        _meshRenderer.materials = _dynamicmaterials;

        //---コルーチンをスタート
        _playCoroutine = StartCoroutine(Run());
    }

    public void Stop()
    {
        //---コルーチンを止める
        if(_playCoroutine != null)
        {
            StopCoroutine(_playCoroutine);
            _playCoroutine = null;
        }

        //---元のマテリアルに戻す
        _meshRenderer.materials = _originalMaterials;
    }

    private void SetAlpha(float value)
    {
        //---実際にアルファ値を変更
        for(int materialIndex = 0;materialIndex < _dynamicmaterials.Length; ++materialIndex)
        {
            //---シェーダーグラフのプロパティを参照
            _dynamicmaterials[materialIndex].SetFloat("_Alpha",value);
        }
    }

    private IEnumerator Run()
    {
        
        //---0秒になるまで繰り返す
        while (timer > 0)
        {

            SetAlpha(timer / 10);               // アルファ値を変更
                                                   //---<memo>
                                                   //   ここでは10.0f / 10 = 1 １をSetAlpha()に渡す
                                                   //   10.0f - DestroyTime = num numを渡す
                                                   //   0秒まで繰り返す

            //---1フレーム休む
            yield return null;

            // 時間経過
            timer -= DestoyTime;
            
        }
        
                
        //---最終的な値を設定
        SetAlpha(0.0f);
        
    }
}
