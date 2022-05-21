using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Vector3 cameraDistanse;

    [SerializeField] private float cameraZ = -100.0f;
    
    private float cameraPosY = 37.5f;

    //画面真ん中から端までのワールド座標での距離
    private float edgetocenter;

    //画面端のオブジェ
    [SerializeField] private GameObject leftObje;      // 左端
    [SerializeField] private GameObject rightObje;     // 右端
    [SerializeField] private GameObject heightObje;    // 上端
    [SerializeField] private GameObject underObje;     // 下端

    //画面端オブジェのポジション
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 heightPos;
    private Vector3 underPos;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        playerObject = GameObject.Find(GameData.Player.name);

        cameraDistanse = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + cameraPosY, cameraZ);
        this.transform.position = cameraDistanse;


        edgetocenter = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector3(0, playerObject.transform.position.y + cameraPosY, 
            -(cameraZ - playerObject.transform.position.z))).x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 
            playerObject.transform.position.y + cameraPosY, -(cameraZ - playerObject.transform.position.z))).x);

        leftPos = leftObje.transform.position;
        rightPos = rightObje.transform.position;
        heightPos = heightObje.transform.position;
        underPos = underObje.transform.position;
    }

    private void LateUpdate()
    {
        TrackingPlayer(playerObject);

        ScreenEdge();

        //Debug.Log("" + (Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -CameraZ))));
    }

    void TrackingPlayer(GameObject playerObj)
    {
        this.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + cameraPosY, cameraZ);
    }

    private void ScreenEdge()
    {
        // 左端
        if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x))
        {
            // 追従を止めたときはカメラを固定
            this.gameObject.transform.position = new Vector3(leftPos.x + edgetocenter, playerObject.transform.position.y + cameraPosY, cameraZ);
        }

        // 右端
        if (edgetocenter > Mathf.Abs(rightPos.x - 2 - playerObject.transform.position.x))
        {
            this.gameObject.transform.position = new Vector3(rightPos.x - edgetocenter, playerObject.transform.position.y + cameraPosY, cameraZ);
        }

        // 上端
        if (edgetocenter > Mathf.Abs(heightPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(playerObject.transform.position.x, heightPos.y - edgetocenter + cameraPosY, cameraZ);
        }

        // 下端
        if (edgetocenter > Mathf.Abs(underPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(playerObject.transform.position.x, underPos.y + edgetocenter + cameraPosY, cameraZ);
        }

        // 左下
        if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(underPos.y + 2 - playerObject.transform.position.y))
        {
            // 追従を止めたときはカメラを固定
            this.gameObject.transform.position = new Vector3(leftPos.x + edgetocenter, underPos.y + edgetocenter + cameraPosY, cameraZ);
        }

        // 右下
        if (edgetocenter > Mathf.Abs(rightPos.x - 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(underPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(rightPos.x - edgetocenter, underPos.y + edgetocenter + cameraPosY, cameraZ);
        }

        // 左上
        if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(heightPos.y + 2 - playerObject.transform.position.y))
        {
            // 追従を止めたときはカメラを固定
            this.gameObject.transform.position = new Vector3(leftPos.x + edgetocenter, heightPos.y - edgetocenter + cameraPosY, cameraZ);
        }

        // 右上
        if (edgetocenter > Mathf.Abs(rightPos.x - 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(heightPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(rightPos.x - edgetocenter, heightPos.y - edgetocenter + cameraPosY, cameraZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
