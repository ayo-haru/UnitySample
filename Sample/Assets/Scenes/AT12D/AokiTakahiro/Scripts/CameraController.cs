using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Vector3 cameraDistanse;

    [SerializeField] private float cameraY = 0.0f;
    [SerializeField] private float cameraZ = -100.0f;

    private bool tracking = false;

    //��ʐ^�񒆂���[�܂ł̃��[���h���W�ł̋���
    private float edgetocenter;

    //��ʒ[�̃I�u�W�F
    [SerializeField] private GameObject leftObje;
    [SerializeField] private GameObject rightObje;
    //��ʒ[�I�u�W�F�̃|�W�V����
    private Vector3 leftPos;
    private Vector3 rightPos;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        playerObject = GameObject.Find(GameData.Player.name);

        cameraDistanse = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, cameraZ);
        this.transform.position = cameraDistanse;


        edgetocenter = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector3(0, playerObject.transform.position.y, -(cameraZ - playerObject.transform.position.z))).x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, playerObject.transform.position.y, -(cameraZ - playerObject.transform.position.z))).x);
        leftPos = leftObje.transform.position;
        rightPos = rightObje.transform.position;
    }

    private void FixedUpdate()
    {
        TrackingPlayer(playerObject);

        ScreenEdge();

        //Debug.Log("" + (Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -CameraZ))));
    }

    void TrackingPlayer(GameObject playerObj)
    {
        this.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, cameraZ);
    }

    private void ScreenEdge()
    {
       //�X�Ƀ��[���h���W��ł̉�ʐ^�񒆂���[�܂ł̋�����茻�݂̃v���C���[����[�܂ł̋������߂���ΒǏ]���~�߂�
       if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x/* - cameraToplayer*/))
       {

           tracking = false;
           //�Ǐ]���~�߂��Ƃ��̓J�������Œ�
           this.gameObject.transform.position = new Vector3(leftPos.x + 15 + edgetocenter, playerObject.transform.position.y, cameraZ);
       }
       else
       {
           tracking = true;
       }

       //�������͉E�[�̎�����
       if (edgetocenter > Mathf.Abs(rightPos.x - 2 - playerObject.transform.position.x/* - cameraToplayer*/))
       {
           tracking = false;
           this.gameObject.transform.position = new Vector3(rightPos.x - 15 - edgetocenter, playerObject.transform.position.y, cameraZ);
       }
       else
       {
           tracking = true;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
