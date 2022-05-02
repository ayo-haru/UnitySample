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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;


public class Player2 : MonoBehaviour
{
    //---変数宣言

    //---プレイヤーのステートの列挙型
    enum PLAYERSTATE 
    {
        NORMAL = 0,         // 0:通常時
        DAMAGED,            // 1:被弾時
        INVISIBLE,          // 2:無敵時

        MAX_STATE
    }

    //---ダメージ処理
    private PLAYERSTATE state = PLAYERSTATE.NORMAL;     // STATE型の変数
    public float WaitTime = 1.0f;                       // 無敵時間
    public float KnockBackPower;                        // ノックバックする時間
    private Vector3 Distination;                        // 被弾時の位置と距離を算出するための変数

    //---振動
    [SerializeField] private float LowFrequency;        // 左側の振動の値
    [SerializeField] private float HighFrequency;       // 右側の振動の値
    [SerializeField] private float VibrationTime;       // 振動時間

    //---InputSystem関連
    [System.NonSerialized]
    public static Game_pad PlayerActionAsset;           // InputActionで生成したものを使用
    private InputAction move;                           // InputActionのmoveを扱う
    private InputAction Attacking;                      // InputActionのmoveを扱う
    private InputAction _Pause;                         // InputActionのpauseを扱う

    //---アニメーション関連
    [SerializeField] private Animator animator;         // アニメーターコンポーネント取得

    //---コンポーネント取得
    private Rigidbody rb;
    public GameObject Weapon;                           // "Weapon"プレハブを格納する変数
    private GameObject hp;                              // HPのオブジェクトを格納
    private GameObject canvas;                          // キャンバスを格納
    private HPManager hpmanager;                        // HPManagerのコンポーネントを取得する変数
    private BoxCollider box_collider;                   // 足元の当たり判定
    private ShieldManager shieldManager;                // 盾の最大数

    //---移動変数
    private Vector3 PlayerPos;                          // プレイヤーの座標
    private Vector2 ForceDirection = Vector2.zero;      // 移動する方向を決める
    private Vector2 MovingVelocity = Vector3.zero;      // 移動するベクトル
    [SerializeField] private float maxSpeed = 50.0f;    // 移動スピード(歩く早さ)
    private Vector2 jumppower;                          // ジャンプ力を保存(盾から渡される)

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
    private Vector2 AttackDirection = Vector2.zero;
    public float AttckPosHeight = 6.0f;                 // シールド位置上下
    public float AttckPosWidth = 4.0f;                  // シールド位置左右
    public float DestroyTime = 0.5f;                    // シールドが消える時間
    private bool isAttack;                              // 攻撃フラグ
    private Vector3 CurrentScale;                       // 現在のプレイヤーのスケールの値を格納 

    //---カメラ
    ShakeCamera shakeCamera;

    /// <summary> ヒットストップ演出関連/// </summary>
    public float HitStopTime = 0.23f;
    public bool CanHitStopflg = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shieldManager = GetComponent<ShieldManager>();
        PlayerActionAsset = new Game_pad();             // InputActionインスタンスを生成
        rb.AddForce(GameData.PlayerVelocyty.Velocity,ForceMode.Impulse);
        shakeCamera = this.GetComponent<ShakeCamera>();
    }
    

    //---ボタンンの入力を結び付ける
    private void OnEnable() {
        //---スティック入力を取るための設定
        move = PlayerActionAsset.Player.Move;
        Attacking = PlayerActionAsset.Player.Attack;
        _Pause = PlayerActionAsset.UI.Start;

        //---Actionイベント登録(ボタン入力)
        PlayerActionAsset.Player.Attack.started += OnAttack;
        Debug.Log(PlayerActionAsset.Player.Attack);
        PlayerActionAsset.UI.Start.canceled += PauseToggle;
        Debug.Log(PlayerActionAsset.UI.Start);

        PlayerActionAsset.Player.Jump.started += OnJump;            // started    ... ボタンが押された瞬間
        //PlayerActionAsset.Player.Jump.performed += OnJump;        // performed  ... 中間くらい
        //PlayerActionAsset.Player.Jump.canceled += OnJump;         // canceled   ... ボタンを離した瞬間

        //回転変数の初期化
        beforeDir = new Vector2(1.0f, 0.0f);

        //---InputActionの有効化(これかかないと、入力とれない。)
        PlayerActionAsset.Player.Enable();
        PlayerActionAsset.UI.Enable();

    }

    private void OnDisable()
    {
        PlayerActionAsset.Player.Attack.started -= OnAttack;        // started ...ボタンが押された瞬間
        PlayerActionAsset.Player.Jump.started -= OnJump;            // started ... ボタンが押された瞬間
        PlayerActionAsset.UI.Start.started -= PauseToggle;          // started ...ボタンが押された瞬間


        //---InputActionの無効化
        PlayerActionAsset.Player.Disable();
        PlayerActionAsset.UI.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        Weapon = (GameObject)Resources.Load("Weapon");
        
        if (GameObject.Find("HPSystem(2)(Clone)")){
            hp = GameObject.Find("HPSystem(2)(Clone)");         // HPSystemを参照
            hpmanager = hp.GetComponent<HPManager>();           // HPSystemの使用するコンポーネント
        }

        if (GameObject.Find("Canvas"))
        {
            canvas = GameObject.Find("Canvas");                 // シーン内のCanvasを検索
        }

        scale = transform.localScale;
        box_collider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {
        //---重力を与える(条件調整中(04/21地点))
        //if (UnderParryNow == true || GroundNow == false || JumpNow == true)
        //{
        //    Gravity();
        //}

        if (!Pause.isPause)
        {
            // プレイヤーがダメージ中は以降の処理はしない
            if (state == PLAYERSTATE.DAMAGED)
            {
                return;
            }

            if (!CanHitStopflg){
                animator.speed = 1.0f;
            }

            //rb.Resume(gameObject);
            GamePadManager.onceTiltStick = false;

            //---HPオブジェクトを検索
            if (!hp)
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
                hpmanager.Damaged();
            }

            //---コントローラーキーでHPを増やす(デバッグ)
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (GameData.CurrentHP < hpmanager.MaxHP)
                {
                    EffectManager.Play(EffectData.eEFFECT.EF_HEAL, this.transform.position);
                    //GameData.CurrentHP++;
                    //SaveManager.saveHP(GameData.CurrentHP);
                    hpmanager.GetItem();
                }
            }

            //Debug.Log("したぱりい"+UnderParryNow);
            //Debug.Log("じゃんぷなう" + JumpNow);
            //Debug.Log("じめんなう" + GroundNow);


            //---地面と当たった時にジャンプフラグ・下パリイフラグを下す
            if (GroundNow)
            {
                if (JumpNow == true || UnderParryNow == true)
                {
                    Debug.Log("着地した");
                    JumpNow = false;
                    UnderParryNow = false;
                }
            }

            if (Player.shouldRespawn)
            {
                animator.StopPlayback();
            }

            GameData.PlayerVelocyty.Set(rb); 
        }
        else
        {
            animator.speed = 0.0f;
            

            rb.Pause(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!Pause.isPause){

            if (UnderParryNow == true || GroundNow == false)
            {
                Gravity();
            }


            Move("Velocity");

            if (isAttack){
                Attack();
            }

        }
        else{
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
                Debug.Log("MovingVelocityの値"+MovingVelocity);

                if (Timer <= 0){
                    rb.velocity = new Vector3(MovingVelocity.x, rb.velocity.y - MovingVelocity.y, 0);
                }
                else{
                    --Timer;
                    rb.velocity = new Vector3(0, rb.velocity.y - MovingVelocity.y, 0);
                }
            break;

            //---AddForceを使用した処理
            case "AddForce":
                SpeedCheck();
                ForceDirection += move.ReadValue<Vector2>();
                ForceDirection.Normalize();
                rb.AddForce((ForceDirection * maxSpeed), ForceMode.Impulse);

                break;

            default:
            break;
        }

        //---アニメーション再生
        if (Mathf.Abs(ForceDirection.x) > 0 && Mathf.Abs(ForceDirection.y) == 0){
            if (!animator.GetBool("Walk")){
                animator.SetBool("Walk", true);
            }
        }
        else if (animator.GetBool("Walk")){
            animator.SetBool("Walk", false);
        }

        //---回転処理処理
        //方向が変わってたらスケールｘを反転
        if ((ForceDirection.x > 0 && beforeDir.x < 0) || (ForceDirection.x < 0 && beforeDir.x > 0)){
            scale.x *= -1;
            transform.localScale = scale;
        }

        //回転の目標値
        Quaternion target = new Quaternion();
        if (ForceDirection.magnitude > 0.01f){
            //方向を保存
            beforeDir = ForceDirection;
            //向きを設定
            target = Quaternion.LookRotation(ForceDirection);
        }
        else{
            //入力されてなかったとき
            //回転が中途半端な時は補正する
            if (transform.rotation.y != 90.0f && transform.rotation.y != -90.0f){
                target = Quaternion.LookRotation(beforeDir); //向きを変更する
            }
        }
        //ゆっくり回転させる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed);

        ForceDirection = Vector2.zero;
        //jumppower = Vector2.zero;
    }
    #endregion
    
    public void SetJumpPower(Vector2 jumpDir)
    {
       jumppower = jumpDir;
    }

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
        PlayerPos = transform.position;                              // 攻撃する瞬間のプレイヤーの座標を取得
        AttackDirection += Attacking.ReadValue<Vector2>();           // スティックの倒した値を取得
        AttackDirection.Normalize();                                 // 取得した値を正規化(ベクトルを１にする)

        //---アニメーション再生
        //---左右パリィ
        if (Mathf.Abs(AttackDirection.x) >= 1){
            //タイマー設定
            Timer = stopTime;
            animator.SetTrigger("Attack");
            //Debug.Log("左右攻撃");
        }

        //---上パリィ
        if (AttackDirection.y > 0){
            if (GroundNow == true){
                rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
            }
            Timer = stopTime;
            animator.SetTrigger("Attack_UP");
        }

        //---下パリィ
        //---y軸が－だったら(下パリィする際)ジャンプ中にする(03/21時点)
        //---y軸が－だったら(下パリィする際)下パリィフラグにする(03/25時点)
        if (AttackDirection.y < 0){
            if (GroundNow == true)
            {
                rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
            }
            Timer = 1;
            rb.velocity = Vector3.zero;
            
            UnderParryNow = true;
            GamePadManager.onceTiltStick = true;
            //GroundNow = false;
            animator.SetTrigger("Attack_DOWN");
            Debug.Log("したはじきした");
		}

		//モデルの向きと反対方向に盾出したらモデル回転
		if ((AttackDirection.x > 0 && beforeDir.x < 0) || (AttackDirection.x < 0 && beforeDir.x > 0)){
            //方向を保存
            beforeDir.x = AttackDirection.x;
            //回転
            transform.rotation = Quaternion.LookRotation(AttackDirection);
            //スケールxを反転
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (AttackDirection.x < 0 && beforeDir.x > 0){
            beforeDir = AttackDirection;
            transform.rotation = Quaternion.LookRotation(AttackDirection);
        }

        //---倒した値を基に盾の出す場所を指定
        GameObject weapon = Instantiate(Weapon, new Vector3(PlayerPos.x + (AttackDirection.x * AttckPosWidth),
                                        PlayerPos.y + (AttackDirection.y * AttckPosHeight),
                                        PlayerPos.z), Quaternion.identity);


        Debug.Log("盾出現"+weapon.transform.position);
        //---コントローラーの倒したXの値が－だったらy軸に-1する(盾の角度の調整)
        if (AttackDirection.x < 0){
            AttackDirection.y *= -1;
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
        //if(JumpNow == true){
        if( UnderParryNow == false){
            return;
        }

        Debug.Log("ジャンプ！");
        JumpNow = true;
        //GroundNow = false;
        rb.AddForce(transform.up * JumpForce,ForceMode.Impulse);
        SoundManager.Play(SoundData.eSE.SE_JUMP, SoundData.GameAudioList);
    }
    #endregion

    #region ダメージ処理
    //===================================================================
    //  ダメージ
    // <memo>
    //      Damage()でアニメーションの再生,HPの減少,EF,SEの再生を行い
    //      Invicible()で無敵中の処理(のけぞり)を行う
    //      無敵時間の処理はコルーチン(InvicibleTIme())で処理する
    //===================================================================
    private void Damaged()
    {
        if (!hp){                        // hpのUIがない場合は処理終了
            return;
        }
        
        // ステートを被弾時に変更
        state = PLAYERSTATE.DAMAGED;
        Debug.Log("攻撃をうけた。");
        if(animator.GetBool("Damagae") == false)
        {
            animator.SetBool("Damage",true);
        }
        EffectManager.Play(EffectData.eEFFECT.EF_DAMAGE, this.transform.position);
        SoundManager.Play(SoundData.eSE.SE_DAMEGE, SoundData.GameAudioList);

        hpmanager.Damaged();
        //GameData.CurrentHP--;
        //SaveManager.saveHP(GameData.CurrentHP);

        Invicible();
    }
    //===================================================================
    //  ダメージ時のリスポーン
    // <memo>
    //  リスポーン時のフラグ処理
    //===================================================================
    private void DamegeRespawn() {
        Player.shouldRespawn = true;
        Pause.isPause = true;
        GameData.isFadeOut = true;
    }

    //===================================================================
    //  無敵中の処理
    //===================================================================
    public void Invicible()
    {
        // 無敵時は処理をしない
        if(state == PLAYERSTATE.INVISIBLE)
        {
            return;
        }

        state = PLAYERSTATE.INVISIBLE;

        // ノックバック処理
        rb.velocity = Vector3.zero;
        ForceDirection = Vector2.zero;

        rb.AddForce(Distination * KnockBackPower,ForceMode.VelocityChange);

        StartCoroutine("InvicibleTime");
    }

    //===================================================================
    //  無敵時間のコルーチン
    //===================================================================
    IEnumerator InvicibleTime()
    {
        yield return new WaitForSeconds(WaitTime);
        state = PLAYERSTATE.NORMAL;
    }
    #endregion

    #region Gravity関数
    //---ジャンプ中の重力を強くする(ジャンプが俊敏に見える効果がある)
    private void Gravity()
    {
        if(/*JumpNow == true || UnderParryNow == true ||*/ GroundNow == false){
            //rb.AddForce(new Vector3(0.0f,GravityForce,0.0f));
            rb.AddForce((transform.up * GravityForce) * Time.deltaTime,ForceMode.Impulse);
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

        if (PlayerVelocity.sqrMagnitude > maxSpeed * maxSpeed){
            rb.velocity = PlayerVelocity.normalized * maxSpeed;
        }
    }
    #endregion

    #region ポーズの入力処理
    private void PauseToggle(InputAction.CallbackContext obj) {
        Pause.isPause = !Pause.isPause; // トグル
    }
    #endregion

    #region 各種あたり判定処理
    //---HPがゼロになった時の処理
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Damaged" ||
            collision.gameObject.tag == "GroundDameged" ||
            collision.gameObject.tag == "Enemy")
        {
            OnAttackHit();
            //---自分の位置と接触してきたオブジェクトの位置を計算し、距離と方向を算出
            Distination = (transform.position - collision.transform.position).normalized;
            // プレイヤーがダメージを食らっていないとき
            if (state == PLAYERSTATE.NORMAL)
            {
                Damaged();
            }
        }
        else
        {
            animator.SetBool("Damage", false);

        }
        if (collision.gameObject.tag == "Damaged" || collision.gameObject.tag == "GroundDameged")
        {
            if (!GameOver.GameOverFlag)
            {
                DamegeRespawn();
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {

    }
    //---当たり判定処理(GroundCheckのボックスコライダーで判定を取るように)
    //private void OnTriggerEnter(Collider other)
    //   {
    //       //---Tag"Ground"と接触している間の処理
    //       if (other.gameObject.tag == "Ground")
    //       {
    //           GroundNow = true;
    //           if (JumpNow == true || UnderParryNow == true)
    //           {
    //               ////足が地面より上だったら着地中にする
    //               //Vector3 footPotision = box_collider.transform.position;
    //               //footPotision.y -= 16.0f;

    //               //if(other.gameObject.transform.position.y <= footPotision.y)
    //               //{
    //               //    Debug.Log("足の位置" + footPotision);
    //               Debug.Log("着地した");
    //               JumpNow = false;
    //               UnderParryNow = false;
    //               //}
    //           }

    //           //ForceDirection = Vector2.zero;
    //           //SoundManager.Play(SoundData.eSE.SE_LAND, SoundData.GameAudioList);
    //       }

    //   }
    //private void OnTriggerEnter(Collider other) {
    //    //---Tag"Ground"と接触している間の処理
    //    if (other.gameObject.tag == "Ground")
    //    {
    //        GroundNow = true;
    //    }

    //}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Ground")
		{
            GroundNow = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ground")
		{
			//JumpNow = true;
			GroundNow = false;

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
        if(gamepad != null){
            gamepad.SetMotorSpeeds(lowFrequency,HighFrequency);
            yield return new WaitForSeconds(VibrationTime);
            gamepad.SetMotorSpeeds(0.0f,0.0f);
        }
    }
	#endregion

    public void OnAttackHit()
    {
        CanHitStopflg = true;
        //---モーションを止める
        animator.speed = 0.0f;

        var seq = DOTween.Sequence();
        seq.SetDelay(HitStopTime);

        seq.AppendCallback(() => animator.speed = 1f);

    }
	#region GUI表示
	private void OnGUI()
    {
        //if (Gamepad.current == null)
        //{
        //    return;
        //}
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
#endregion

