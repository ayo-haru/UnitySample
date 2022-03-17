//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody))]
//public class ControllerMove : MonoBehaviour
//{
//    //---変数宣言
//    private GamePad _gamepad;
//    private Vector2 _moveInputValue;
//    private Rigidbody _rigidbody;

//    [SerializeField] private float _moveForce = 5.0f;
//    [SerializeField] private float _jumpForce = 5.0f;


    
    
//    /*平たくいうとstartメソッドよりも前に呼び出される*/
//    void Awake()
//    { 

//        _rigidbody = GetComponent<Rigidbody>();

//        //---Input Actionインスタンスを作成
//        _gamepad = new GamePad();

//        //---Actionイベント登録
//        _gamepad.Player.Move.started += OnMove;
//        _gamepad.Player.Move.performed += OnMove;
//        _gamepad.Player.Move.canceled += OnMove;
//        _gamepad.Player.Jump.performed += OnMove;

//        //---Input Actionを機能させるためには、有効化する必要がある
//        _gamepad.Enable();

//    }

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //---移動方向の力を与える
//        _rigidbody.AddForce(new Vector3(_moveInputValue.x, 0, _moveInputValue.y) * _moveForce);

//    }

//    //private void FixedUpdate()
//    //{
//    /*fixedUpdateは毎秒呼ばれる処理(プロジェクト設定から値を設定)*/
//    //    //---移動方向の力を与える
//    //    _rigidbody.AddForce(new Vector3(_moveInputValue.x,0,_moveInputValue.y) * _moveForce);
//    //}


//    private void OnMove(InputAction.CallbackContext context)
//    {
//        //---Moveアクションの入力取得
//        _moveInputValue = context.ReadValue<Vector2>();
//    }

//    private void OnJump(InputAction.CallbackContext context)
//    {
//        //---Jumpアクション(ジャンプする力を与える)
//        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
//    }

//}