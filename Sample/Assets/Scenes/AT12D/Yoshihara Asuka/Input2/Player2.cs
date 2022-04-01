//=============================================================================
//
// コントローラー対応Player
//
// 作成日:2022/03/18
// 作成者:吉原飛鳥
//
// <開発履歴>
// 2022/03/18   作成
// 2022/03/20   移動、攻撃実施(この時点ではAddForce)
// 2022/03/25   プレイヤーの挙動修正(移動をVelocity計算に変更)
// 2022/03/25   プレイヤーの挙動修正(ジャンプ中の挙動変更ストレイフアリ)
// 2022/04/01   アニメーション導入
// 2022/04/01   移動ぷかぷかさせるやつ
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---変数宣言

    //---InputSystem関連
    private Game_pad PlayerActionAsset;                 // InputActionで生成したものを使用
    private InputAction move;                           // InputActionのmoveを扱う
    private InputAction Attack;                         // InputActionのmoveを扱う

    //---アニメーション関連
    [SerializeField] public Animator animator;          // アニメーターコンポーネント取得


    //---コンポーネント取得
    private Rigidbody rb;
    public GameObject prefab;                           // "Weapon"プレハブを格納する変数
    GameObject hp;                                      // HPのオブジェクトを格納
    HPManager hpmanager;                                // HPManagerのコンポーネントを取得する変数

    //---移動変数
    private Vector3 PlayerPos;                          // プレイヤーの座標
    private Vector2 ForceDirection = Vector2.zero;      // 移動する方向を決める
    private Vector2 MovingVelocity = Vector3.zero;      // 移動するベクトル
    [SerializeField] private float maxSpeed = 5;        // 移動スピード(歩く早さ)

    public int stopTime = 20;                           //盾出したときに止まる時間
    private int Timer = 0;                              //停止時間計測用

    public float Amplitude;                             // 振れ幅
    private int FylFrameCount;                         //　フワフワしてる処理に使うフレームカウント

    //---回転変数
    private Vector2 beforeDir;                           //最後に入力された方向
    public float rotationSpeed = 30.0f;                  //回転スピード
    private Vector3 scale;                              //スケール

    //---ジャンプ変数
    public float GravityForce = -10.0f;                 // 重力
    private bool JumpNow = false;                       // ジャンプしているかどうか
    private bool UnderParryNow = false;                 // 下パリィ中かどうか(
    [SerializeField] private float JumpForce = 5;       // ジャンプ力


    //---攻撃変数
    private Vector2 AttackDirection = Vector2.zero;     // 攻撃方向
    public float AttckPosHeight = 8.0f;                 // シールド位置上下
    public float AttckPosWidth = 8.0f;                  // シールド位置左右
    public float DestroyTime = 3.0f;                    // シールドが消える時間
    private Vector3 CurrentScale;                       // 現在のプレイヤーのスケールの値を格納 



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerActionAsset = new Game_pad();             // InputActionインスタンスを生成
    }


    //---ボタンンの入力を結び付ける
    private void OnEnable()
    {
        //---スティック入力を取るための設定
        move = PlayerActionAsset.Player.Move;           
        Attack = PlayerActionAsset.Player.Attack;

        //---Actionイベント登録(ボタン入力)
        PlayerActionAsset.Player.Attack.started += OnAttack;

        PlayerActionAsset.Player.Jump.started += OnJump;            // started    ... ボタンが押された瞬間
        //PlayerActionAsset.Player.Jump.performed += OnJump;        // performed  ... 中間くらい
        //PlayerActionAsset.Player.Jump.canceled += OnJump;         // canceled   ... ボタンを離した瞬間

        //回転変数の初期化
        beforeDir = new Vector2(1.0f, 0.0f);

        //---InputActionの有効化(これかかないと、入力とれない。)
        PlayerActionAsset.Player.Enable();

    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= OnAttack;        // started...ボタンが押された瞬間
        PlayerActionAsset.Player.Jump.started -= OnJump;            // started    ... ボタンが押された瞬間


        //---InputActionの無効化
        PlayerActionAsset.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        prefab = (GameObject)Resources.Load("Weapon");


        if (GameObject.Find("HPSystem(Clone)"))
        {
            hp = GameObject.Find("HPSystem(Clone)");        // HPSystemを参照
            hpmanager = hp.GetComponent<HPManager>();       // HPSystemの使用するコンポーネント
        }
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update() 
    {
        //---rb.velocityによる移動処理
        ForceDirection += move.ReadValue<Vector2>();
        ForceDirection.Normalize();
        MovingVelocity = ForceDirection * maxSpeed;

        //---アニメーション再生
        if (Mathf.Abs(ForceDirection.x) > 0 && Mathf.Abs(ForceDirection.y) == 0)
        {
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
                
            }
        }
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        //---HPオブジェクトを検索
        if (!GameObject.Find("HPSystem(Clone)"))
        {
            return;
        }

        //---バックスペースキーでHPを減らす(デバッグ)
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameData.CurrentHP--;
            EffectManager.Play(EffectData.eEFFECT.EF_DAMAGE, this.transform.position);
            SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);
        }
        //---コントローラーキーでHPを増やす(デバッグ)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(GameData.CurrentHP < hpmanager.MaxHP)
            {
                EffectManager.Play(EffectData.eEFFECT.EF_HEAL, this.transform.position);
                GameData.CurrentHP++;
            }

        }
    }

    private void FixedUpdate()
    {
        //---ジャンプ中なら移動処理をしない
        if(JumpNow == true)
        {
            Gravity(); 
            //return;
        }

        //---移動処理(AddForceの処理)
        //SpeedCheck();
        //ForceDirection += move.ReadValue<Vector2>();
        //ForceDirection.Normalize();
        //rb.AddForce(ForceDirection * maxSpeed, ForceMode.Impulse);

        //---移動処理(velocityの処理)
        if (Timer <= 0)
        {
            rb.velocity = new Vector3(MovingVelocity.x, rb.velocity.y - MovingVelocity.y, 0);
        }
        else
        {
            --Timer;
            rb.velocity = new Vector3(0, rb.velocity.y - MovingVelocity.y, 0);
        }

        //---回転処理処理
        //方向が変わってたらスケールｘを反転
        if((ForceDirection.x > 0 && beforeDir.x < 0)||(ForceDirection.x < 0 && beforeDir.x > 0)){
            scale.x *= -1;
            transform.localScale = scale;
        }
        //回転の目標値
        Quaternion target = new Quaternion();
        if (ForceDirection.magnitude > 0.01f)
        {
            //方向を保存
            beforeDir = ForceDirection;
            //向きを設定
            target = Quaternion.LookRotation(ForceDirection); 
        }
        else
        {
            //入力されてなかったとき
            //回転が中途半端な時は補正する
            if(transform.rotation.y != 90.0f && transform.rotation.y != -90.0f)
            {
                target = Quaternion.LookRotation(beforeDir); //向きを変更する
            }
        }
        //ゆっくり回転させる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed);

        ForceDirection = Vector2.zero;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {

    }

    //---攻撃処理
    private void OnAttack(InputAction.CallbackContext obj)
    {
        //タイマー設定
        Timer = stopTime;

        //---スティック入力
        PlayerPos = transform.position;                             // 攻撃する瞬間のプレイヤーの座標を取得
        AttackDirection += Attack.ReadValue<Vector2>();             // スティックの倒した値を取得
        Debug.Log("AttackDirection(正規化前):" + AttackDirection);
        AttackDirection.Normalize();                                // 取得した値を正規化(ベクトルを１にする)

        //---アニメーション再生
        if(Mathf.Abs(AttackDirection.x) > 0)
        {
            animator.SetTrigger("Attack");
        }

        if(AttackDirection.y > 0.1)
        {
            animator.SetTrigger("Attack_UP");
        }


        //モデルの向きと反対方向に盾出したらモデル回転
        if ((AttackDirection.x > 0 && beforeDir.x < 0) || (AttackDirection.x < 0 && beforeDir.x > 0))
        {
            //方向を保存
            beforeDir.x = AttackDirection.x;
            //回転
            transform.rotation = Quaternion.LookRotation(AttackDirection);
            //スケールxを反転
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (AttackDirection.x < 0 && beforeDir.x > 0)
        {
            beforeDir = AttackDirection;
            transform.rotation = Quaternion.LookRotation(AttackDirection);
        }

        //---倒した値を基に盾の出す場所を指定
        GameObject weapon = Instantiate(prefab,new Vector3(PlayerPos.x + (AttackDirection.x * AttckPosWidth),
                                                           PlayerPos.y + (AttackDirection.y * AttckPosHeight),
                                                           PlayerPos.z),Quaternion.identity);

        //---コントローラーの倒したXの値が－だったらy軸に-1する(盾の角度の調整)
        if (AttackDirection.x < 0.0f)
        {
            AttackDirection.y *= -1;

        }
        //---y軸が－だったら(下パリィする際)ジャンプ中にする(03/21時点)
        //---y軸が－だったら(下パリィする際)下パリィフラグにする(03/25時点)
        if (AttackDirection.y < 0.0f)
        {
            UnderParryNow = true;
        }
        
        //---盾の回転を設定
        weapon.transform.Rotate(new Vector3(0,0,(90 * AttackDirection.y)));
        //Debug.Log("攻撃した！(Weapon)");
        Debug.Log("AttackDirection(正規化後):"+ AttackDirection);
        //EffectManager.Play(EffectData.eEFFECT.EF_SHEILD2,weapon.transform.position);
        //SoundManager.Play(SoundData.eSE.SE_SHIELD, SoundData.GameAudioList);
        AttackDirection = Vector2.zero;                           // 入力を取る度、新しい値が欲しいため一度０にする
        Destroy(weapon,DestroyTime);
        return;
    }

    //---ジャンプ処理
    private void OnJump(InputAction.CallbackContext obj)
    {
        //---ジャンプ中であればジャンプしない
        if(JumpNow == true)
        {
            return;
        }
        Debug.Log("ジャンプ！");
        JumpNow = true;
        rb.AddForce(transform.up * JumpForce,ForceMode.Impulse);
        SoundManager.Play(SoundData.eSE.SE_JUMP, SoundData.GameAudioList);
    }

    //---ジャンプ中の重力を強くする(ジャンプが俊敏に見える効果がある)
    private void Gravity()
    {
        if(JumpNow == true)
        {
            rb.AddForce(new Vector3(0.0f,GravityForce,0.0f));
            //ForceDirection = Vector2.zero;
        }
    }

    //---オブジェクトをフワフワさせる処理
    private void FluffyMove()
    {
        FylFrameCount += 1;
        if(1000 <= FylFrameCount)
        {
            FylFrameCount = 0;
        }
        if(0 == FylFrameCount % 2)
        {
            float posYSin = Mathf.Sin(2.0f * Mathf.PI * (float)(FylFrameCount % 200) / (200.0f - 1.0f));
            iTween.MoveAdd(gameObject, new Vector3(0, Amplitude * posYSin, 0), 0.0f);
        } 
    }

    //---AddForceで力がかかりすぎてしまうため、maxSpeedの値に近い値に固定する処理
    private void SpeedCheck()
    {
        Vector3 PlayerVelocity = rb.velocity;
        PlayerVelocity.z = 0;

        if (PlayerVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = PlayerVelocity.normalized * maxSpeed;
        }
    }

    //---当たり判定処理
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Damaged")
        {
            Debug.Log("攻撃をうけた。");
            EffectManager.Play(EffectData.eEFFECT.EF_DAMAGE, this.transform.position);
            SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);
            if (!GameObject.Find("HPSystem(Clone)"))
            {
                return;
            }
            GameData.CurrentHP--;

            //HPが0になったらゲームオーバーを表示
            if(GameData.CurrentHP <= 0)
            {
                GameObject.Find("Canvas").GetComponent<GameOver>().GameOverShow();
            }
        }
    }

    //---当たり判定処理(GroundCheckのボックスコライダーで判定を取るように)
    private void OnTriggerEnter(Collider other)
    {
        if (JumpNow == true || UnderParryNow == true)
        {
            //---Tag"Ground"と接触している間の処理
            if (other.gameObject.tag == "Ground")
            {
                Debug.Log("着地中");
                JumpNow = false;
                UnderParryNow = false;
                //ForceDirection = Vector2.zero;
                //SoundManager.Play(SoundData.eSE.SE_LAND, SoundData.GameAudioList);
            }
        }
    }


    private void OnGUI()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        //---ゲームパッドとつながっている時に表示される。
        GUILayout.Label($"LeftStick:{Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"RightStick:{Gamepad.current.rightStick.ReadValue()}");
        GUILayout.Label($"ButtonNorth:{Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"ButtonSouth:{Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"ButtonEast:{Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"ButtonWast:{Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"LeftShoulder:{Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"LeftTrigger:{Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"RightShoulder:{Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"RighetTrigger:{Gamepad.current.rightTrigger.ReadValue()}");
        GUILayout.Label($"LeftStickUp:{Gamepad.current.leftStick.up.ReadValue()}");
        GUILayout.Label($"Space:{Keyboard.current.spaceKey.ReadValue()}");
        GUILayout.Label($"JumpFlg:{JumpNow}");
    }

}

