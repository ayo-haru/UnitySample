//==========================================================
//      UI弾いた時の処理
//      作成日　2022/05/08
//      作成者　海川晃楊
//      
//      <開発履歴>
//      2022/05/08
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIParry : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;

    public bool isAlive;                    
    private float Timer;                    // 時間格納用
    private float DestroyTime = 0.0f;       // 消えるまでの時間
    private float bouncePower = 200.0f;     // 吹き飛ぶ強さ

    //---ヒットストップ演出(2022/05/02.吉原)
    Player2 player2;
    public float Width = 0.1f;
    public int RoundCnt = 4;
    public float Duration = 0.23f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        player2 = Player.GetComponent<Player2>();
    }

    // Update is called once per frame
    void Update()
    {
        // 時間で消える処理
        if (!isAlive)
        {
            DestroyTime += Time.deltaTime;
        }
        if (DestroyTime > 1.5f)
        {
            Destroy(gameObject, 0.0f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // 弾かれたらベクトルを計算して関数を呼び出す
        if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
        {
            vec = (transform.position - Player.transform.position).normalized;

            //---ヒットストップ演出
            var seq = DOTween.Sequence();
            //---UIの振動演出
            seq.Append(transform.DOShakePosition(player2.HitStopTime, 1f, 100, fadeOut: false));

            //---このタイミングでUIを飛ばす処理を呼び出す
            seq.AppendCallback(() => UIDestroy(vec, Player.transform.position.x));
            Shake(0.1f, 5, 0.23f);
        }
    }

    // UIを飛ばす処理
    public void UIDestroy(Vector3 vec, float x)
    {
        //プレイヤーと逆方向に跳ね返す
        rb.velocity = vec * bouncePower;

        // 手前に吹き飛ばす
        //rb.velocity = new Vector3(0.0f, 0.0f, -bouncePower);

        // 回転させる
        if (x < transform.position.x)
        {
            rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);
        }
        if (x > transform.position.x)
        {
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
        }

        SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);

        isAlive = false;
    }
    /// <summary>
    /// カメラ振動演出
    /// </summary>
    /// <param name="width"></param>    カメラの振れ幅
    /// <param name="cnt"></param>      往復回数
    /// <param name="duration"></param> 時間
    public void Shake(float width, int cnt, float duration)
    {
        var camera = Camera.main.transform;
        var seq = DOTween.Sequence();

        var partDuration = duration / cnt / 2f;

        var widthHalf = width / 2f;

        for (int i = 0; i < cnt - 1; i++)
        {
            seq.Append(camera.DOLocalRotate(new Vector3(-width, 0f), partDuration));
            seq.Append(camera.DOLocalRotate(new Vector3(width, 0f), partDuration));
        }

        seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf, 0f), partDuration));
        seq.Append(camera.DOLocalRotate(Vector3.zero, partDuration));
    }
}
