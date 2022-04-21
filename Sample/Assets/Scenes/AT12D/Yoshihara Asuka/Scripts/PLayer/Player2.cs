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
// 2022/04/02   下パリィ導入   
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    //---変数宣言


    //---振動
    [SerializeField] private float LowFrequency;        // 左側の振動の値
    [SerializeField] private float HighFrequency;       // 右側の振動の値
    [SerializeField] private float VibrationTime;       // 振動時間

    //---InputSystem関連
    public static Game_pad PlayerActionAsset;           // InputActionで生成したものを使用
    private InputAction move;                           // InputActionのmoveを扱う
    private InputAction Attacking;                      // InputActionのmoveを扱う

    //---アニメーション関連
    public Animator animator;                           // アニメーターコンポーネント取得

    


    //---コンポーネント取得
    private Rigidbody rb;
    public GameObject Weapon;                           // "Weapon"プレハブを格納する変数
    GameObject hp;                                      // HPのオブジェクトを格納
    HPManager hpmanager;                                // HPManagerのコンポーネントを取得する変数
    BoxCollider box_collider;                           // 足元の当たり判定

    //---移動変数
    private Vector3 PlayerPos;                          // プレイヤーの座標
    private Vector2 ForceDirection = Vector2.zero;      // 移動する方向を決める
    private Vector2 MovingVelocity = Vector3.zero;      // 移動するベクトル
    [SerializeField] private float maxSpeed = 50.0f;    // 移動スピード(歩く早さ)

    public int stopTime = 5;                            //盾出したときに止まる時間
    private int Timer = 0;                              //停止時間計測用

    public float Amplitude;                             // 振れ幅
    private int FylFrameCount;                          // フワフワしてる処理に使うフレームカウント

    //---回転変数
    private Vector2 beforeDir;                          //最後に入力された方向
    public float rotationSpeed = 30.0f;                 //回転スピード
    private Vector3 scale;                              //スケール

    //---ジャンプ変数
    public float GravityForce = -50.0f;                         // 重力
    [System.NonSerialized]public bool JumpNow = false;          // ジャンプしているかどうか
    [System.NonSerialized] public bool UnderParryNow = false;   // 下パリィ中かどうか
    [System.NonSerialized] public bool GroundNow = false;       // 地面と接地中かどうか
    [SerializeField] private float JumpForce = 5;               // ジャンプ力

    //---攻撃変数
    public Vector2 AttackDirection = Vector2.zero;     // 攻撃方向
    public float AttckPosHeight = 6.0f;                 // シールド位置上下
    public float AttckPosWidth = 4.0f;                  // シールド位置左右
    public float DestroyTime = 0.5f;                    // シールドが消える時間
    private bool isAttack;                              // 攻撃フラグ
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
        Attacking = PlayerActionAsset.Player.Attack;

        //---Actionイベント登録(ボタン入力)
        PlayerActionAsset.Player.Attack.started += OnAttack;

        //PlayerActionAsset.Player.Jump.started += OnJump;            // started    ... ボタンが押された瞬間
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
        //PlayerActionAsset.Player.Jump.started -= OnJump;          // started    ... ボタンが押された瞬間

        //---InputActionの無効化
        PlayerActionAsset.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        Weapon = (GameObject)Resources.Load("Weapon");
        
        if (GameObject.Find("HPSystem(2)(Clone)"))
        {
            hp = GameObject.Find("HPSystem(2)(Clone)");         // HPSystemを参照
            hpmanager = hp.GetComponent<HPManager>();           // HPSystemの使用するコンポーネント
        }
        scale = transform.localScale;

        box_collider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (UnderParryNow == true || GroundNow == true)
        {
            Gravity();
        }

        if (!Pause.isPause)
        {
            GamePadManager.onceTiltStick = false;

            if (isAttack)
            {
                Attack();
            }

            //---HPオブジェクトを検索
            if (!GameObject.Find("HPSystem(2)(Clone)"))
            {
                return;
            }

            //---バックスペースキーでHPを減らす(デバッグ)
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GameData.CurrentHP--;
                //SaveManager.saveHP(GameData.CurrentHP);
                EffectManager.Play(EffectData.eEFFECT.EF_DAMAGE, this.transform.position);
                SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);
            }
            //---コントローラーキーでHPを増やす(デバッグ)
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (GameData.CurrentHP < hpmanager.MaxHP)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_HEAL, this.transform.position);
                    GameData.CurrentHP++;
                    //SaveManager.saveHP(GameData.CurrentHP);
                }

            }
        }
    }

    private void FixedUpdate()
    {
        if (!Pause.isPause)
        {
            animator.speed = 1.0f;

            //---ジャンプ中なら移動処理をしない
            if (JumpNow == true || UnderParryNow == true)
            {
                Gravity();
                //return;
            }

            Move("Velocity");
        }
        else
        {
            animator.speed = 0.0f;
        }

        if (this.gameObject.transform.position.y > 250)
        {
            transform.position = new Vector3(transform.position.x, 250.0f, transform.position.z);
        }
    }

    #region 移動処理
    //===================================================================
    //  移動処理
    //===================================================================
    private void Move(string ModeName)
    {
        switch (ModeName)
        { 
            //---Velocityを使用した移動処理
            case "Velocity":
                ForceDirection += move.ReadValue<Vector2>();
                ForceDirection.Normalize();
                MovingVelocity = ForceDirection * maxSpeed;
                if (Timer <= 0)
                {
                    rb.velocity = new Vector3(MovingVelocity.x, rb.velocity.y - MovingVelocity.y, 0);
                }
                else
                {
                    --Timer;
                    rb.velocity = new Vector3(0, rb.velocity.y - MovingVelocity.y, 0);
                }
            break;

            //---AddForceを使用した処理
            case "AddForce":
                SpeedCheck();
                ForceDirection += move.ReadValue<Vector2>();
                ForceDirection.Normalize();
                rb.AddForce(ForceDirection * maxSpeed, ForceMode.Impulse);
            break;

            default:
            break;
        }

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

        //---回転処理処理
        //方向が変わってたらスケールｘを反転
        if ((ForceDirection.x > 0 && beforeDir.x < 0) || (ForceDirection.x < 0 && beforeDir.x > 0))
        {
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
            if (transform.rotation.y != 90.0f && transform.rotation.y != -90.0f)
            {
                target = Quaternion.LookRotation(beforeDir); //向きを変更する
            }
        }
        //ゆっくり回転させる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed);

        ForceDirection = Vector2.zero;

    }
    #endregion

    #region 攻撃処理
    //===================================================================
    //
    //  攻撃
    //  OnAttackとAttackでセット。OnAttackで攻撃フラグを立てて、
    //  Attackの中に攻撃の処理を書く
    //
    //===================================================================
    private void OnAttack(InputAction.CallbackContext obj)
    {
        isAttack = true;       
    }
    private void Attack() {

        //---振動させる
        //StartCoroutine(VibrationPlay(LowFrequency,HighFrequency));

        //---スティック入力
        PlayerPos = transform.position;                                             // 攻撃する瞬間のプレイヤーの座標を取得
        AttackDirection += Attacking.ReadValue<Vector2>();             // スティックの倒した値を取得
        //AttackDirection.Normalize();                                               // 取得した値を正規化(ベクトルを１にする)

        //---アニメーション再生
        //---左右パリィ
        if (Mathf.Abs(AttackDirection.x) >= 1)
        {
            //タイマー設定
            Timer = stopTime;
            animator.SetTrigger("Attack");
            Debug.Log("左右攻撃");
        }

        //---上パリィ
        if (AttackDirection.y >= 1)
        {
            if (GroundNow == true)
            {
                rb.AddForce(transform.up * 3.0f, ForceMode.Impulse);
                GroundNow = false;
            }
            animator.SetTrigger("Attack_UP");
        }

		//---下パリィ
		if (AttackDirection.y < 0)
		{
			if (GroundNow == true)
			{
				rb.AddForce(transform.up * 3.0f, ForceMode.Impulse);
				GroundNow = false;
			}
			animator.SetTrigger("Attack_DOWN");

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
        GameObject weapon = Instantiate(Weapon, new Vector3(PlayerPos.x + (AttackDirection.x * AttckPosWidth),
                                                           PlayerPos.y + (AttackDirection.y * AttckPosHeight),
                                                           PlayerPos.z), Quaternion.identity);

        //---コントローラーの倒したXの値が－だったらy軸に-1する(盾の角度の調整)
        if (AttackDirection.x < 0)
        {
            AttackDirection.y *= -1;

        }
        //---y軸が－だったら(下パリィする際)ジャンプ中にする(03/21時点)
        //---y軸が－だったら(下パリィする際)下パリィフラグにする(03/25時点)
        if (AttackDirection.y < 0)
        {
            UnderParryNow = true;
            GamePadManager.onceTiltStick = true;
            GroundNow = false;
        }

        //---盾の回転を設定
        weapon.transform.Rotate(new Vector3(0, 0, (90 * AttackDirection.y)));
        //Debug.Log("攻撃した！(Weapon)");
        //EffectManager.Play(EffectData.eEFFECT.EF_SHEILD2,weapon.transform.position);
        SoundManager.Play(SoundData.eSE.SE_SHIELD, SoundData.GameAudioList);
        AttackDirection = Vector2.zero;                           // 入力を取る度、新しい値が欲しいため一度０にする
        Destroy(weapon, DestroyTime);


        isAttack = false;
    }
    #endregion

    #region ジャンプ処理
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
        GroundNow = false;
        rb.AddForce(transform.up * JumpForce,ForceMode.Impulse);
        SoundManager.Play(SoundData.eSE.SE_JUMP, SoundData.GameAudioList);
    }
    #endregion

    #region Gravity関数
    //---ジャンプ中の重力を強くする(ジャンプが俊敏に見える効果がある)
    private void Gravity()
    {
        if(JumpNow == true || UnderParryNow == true)
        {
            rb.AddForce(new Vector3(0.0f,GravityForce,0.0f));
            //rb.position += transform.up * GravityForce;
            //ForceDirection = Vector2.zero;
        }
    }
    #endregion

    #region ふわふわ移動
    //---オブジェクトをフワフワさせる処理
    //private void FluffyMove()
    //{
    //    FylFrameCount += 1;
    //    if(1000 <= FylFrameCount)
    //    {
    //        FylFrameCount = 0;
    //    }
    //    if(0 == FylFrameCount % 2)
    //    {
    //        float posYSin = Mathf.Sin(2.0f * Mathf.PI * (float)(FylFrameCount % 200) / (200.0f - 1.0f));
    //        iTween.MoveAdd(gameObject, new Vector3(0, Amplitude * posYSin, 0), 0.0f);
    //    } 
    //}
    #endregion

    #region 速度調整関数(AddForce移動用)
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
    #endregion

    #region あたり判定処理
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
            hpmanager.Damaged();
            //GameData.CurrentHP--;
            //SaveManager.saveHP(GameData.CurrentHP);

        //HPが0になったらゲームオーバーを表示
        if (GameData.CurrentHP <= 0)
            {
                GameObject.Find("Canvas").GetComponent<GameOver>().GameOverShow();
            }
        }
    }

    //---当たり判定処理(GroundCheckのボックスコライダーで判定を取るように)
    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "Ground")
         {
            GroundNow = true;
            //---Tag"Ground"と接触している間の処理
            if (JumpNow == true || UnderParryNow == true)
            {
                ////足が地面より上だったら着地中にする
                //Vector3 footPotision = box_collider.transform.position;
                //footPotision.y -= 16.0f;
                
                //if(other.gameObject.transform.position.y <= footPotision.y)
                //{
                //    Debug.Log("足の位置" + footPotision);
                    Debug.Log("着地中");
                    JumpNow = false;
                    UnderParryNow = false;
                //}

            }
          //ForceDirection = Vector2.zero;
          //SoundManager.Play(SoundData.eSE.SE_LAND, SoundData.GameAudioList);
   
         }

    }
    #endregion 

    #region コントローラー振動
    //---コントローラー振動処理
    private IEnumerator VibrationPlay
    (
        float lowFrequency,     // 低周波(左) モーターの強さ(0.0 ~ 1.0)
        float HighFrequency     // 高周波(右) モータ-の強さ(0.0 ~ 1.0)
    )
    {
        Gamepad gamepad = Gamepad.current;
        if(gamepad != null)
        {
            gamepad.SetMotorSpeeds(lowFrequency,HighFrequency);
            yield return new WaitForSeconds(VibrationTime);
            gamepad.SetMotorSpeeds(0.0f,0.0f);
        }
    }
    #endregion

    private void OnGUI()
    {
        if (Gamepad.current == null)
        {
            return;
        }
        //---ゲームパッドとつながっている時に表示される。
        //GUILayout.Label($"LeftStick:{Gamepad.current.leftStick.ReadValue()}");
        //GUILayout.Label($"RightStick:{Gamepad.current.rightStick.ReadValue()}");
        //GUILayout.Label($"ButtonNorth:{Gamepad.current.buttonNorth.isPressed}");
        //GUILayout.Label($"ButtonSouth:{Gamepad.current.buttonSouth.isPressed}");
        //GUILayout.Label($"ButtonEast:{Gamepad.current.buttonEast.isPressed}");
        //GUILayout.Label($"ButtonWast:{Gamepad.current.buttonWest.isPressed}");
        //GUILayout.Label($"LeftShoulder:{Gamepad.current.leftShoulder.ReadValue()}");
        //GUILayout.Label($"LeftTrigger:{Gamepad.current.leftTrigger.ReadValue()}");
        //GUILayout.Label($"RightShoulder:{Gamepad.current.rightShoulder.ReadValue()}");
        //GUILayout.Label($"RighetTrigger:{Gamepad.current.rightTrigger.ReadValue()}");
        //GUILayout.Label($"LeftStickUp:{Gamepad.current.leftStick.up.ReadValue()}");
        //GUILayout.Label($"Space:{Keyboard.current.spaceKey.ReadValue()}");
        GUILayout.Label($"JumpFlg:{JumpNow}");
        GUILayout.Label($"GroudFlg:{GroundNow}");
        GUILayout.Label($"UnderParryFlg:{UnderParryNow}");
    }
}

