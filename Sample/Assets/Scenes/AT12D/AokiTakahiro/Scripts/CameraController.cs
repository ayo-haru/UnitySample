using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Vector3 cameraDistanse;

    [SerializeField] private float cameraZ = -100.0f;
    
    private float cameraPosY = 37.5f;

    //��ʐ^�񒆂���[�܂ł̃��[���h���W�ł̋���
    private float edgetocenter;

    //��ʒ[�̃I�u�W�F
    [SerializeField] private GameObject leftObje;      // ���[
    [SerializeField] private GameObject rightObje;     // �E�[
    [SerializeField] private GameObject heightObje;    // ��[
    [SerializeField] private GameObject underObje;     // ���[

    //��ʒ[�I�u�W�F�̃|�W�V����
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
        // ���[
        if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x))
        {
            // �Ǐ]���~�߂��Ƃ��̓J�������Œ�
            this.gameObject.transform.position = new Vector3(leftPos.x + edgetocenter, playerObject.transform.position.y + cameraPosY, cameraZ);
        }

        // �E�[
        if (edgetocenter > Mathf.Abs(rightPos.x - 2 - playerObject.transform.position.x))
        {
            this.gameObject.transform.position = new Vector3(rightPos.x - edgetocenter, playerObject.transform.position.y + cameraPosY, cameraZ);
        }

        // ��[
        if (edgetocenter > Mathf.Abs(heightPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(playerObject.transform.position.x, heightPos.y - edgetocenter + cameraPosY, cameraZ);
        }

        // ���[
        if (edgetocenter > Mathf.Abs(underPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(playerObject.transform.position.x, underPos.y + edgetocenter + cameraPosY, cameraZ);
        }

        // ����
        if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(underPos.y + 2 - playerObject.transform.position.y))
        {
            // �Ǐ]���~�߂��Ƃ��̓J�������Œ�
            this.gameObject.transform.position = new Vector3(leftPos.x + edgetocenter, underPos.y + edgetocenter + cameraPosY, cameraZ);
        }

        // �E��
        if (edgetocenter > Mathf.Abs(rightPos.x - 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(underPos.y + 2 - playerObject.transform.position.y))
        {
            this.gameObject.transform.position = new Vector3(rightPos.x - edgetocenter, underPos.y + edgetocenter + cameraPosY, cameraZ);
        }

        // ����
        if (edgetocenter > Mathf.Abs(leftPos.x + 2 - playerObject.transform.position.x) && edgetocenter > Mathf.Abs(heightPos.y + 2 - playerObject.transform.position.y))
        {
            // �Ǐ]���~�߂��Ƃ��̓J�������Œ�
            this.gameObject.transform.position = new Vector3(leftPos.x + edgetocenter, heightPos.y - edgetocenter + cameraPosY, cameraZ);
        }

        // �E��
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
