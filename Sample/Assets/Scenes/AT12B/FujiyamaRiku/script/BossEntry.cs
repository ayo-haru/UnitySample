using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEntry : MonoBehaviour
{
    Vector3 StartCameraPos;
    Vector3 CameraPos;
    Vector3 CamEndPos;
    CameraMove MoveCam;
    [SerializeField] public Image BossName;
    [SerializeField] public Image BossBarName;
    [SerializeField] public Image BossHPFrame;
    [SerializeField] public Image BossHPBar;
    [SerializeField] float DelTime;
    [SerializeField] float FocusTime;
    [SerializeField] float FocusDelTime;
    bool StartCamera;
    // Start is called before the first frame update
    void Start()
    {
        StartCamera = false;
        CameraPos = Camera.main.transform.position;
        MoveCam = GetComponent<CameraMove>();
        StartCameraPos = GameData.PlayerPos;
        StartCameraPos.z = CameraPos.z;
        CamEndPos = Boss1Manager.BossPos;
        CamEndPos.z = CameraPos.z;
        BossBarName.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        BossHPBar.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        BossHPFrame.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Entry()
    {
        
            if (GameData.PlayerPos.x >= GameObject.Find("CameraOn").transform.position.x && !StartCamera)
            {
                GameObject.Find("Main Camera").GetComponent<DelayFollowCamera>().enabled = false;
                StartCameraPos = GameData.PlayerPos;
                StartCameraPos.z = CameraPos.z;
                StartCamera = true;
                MoveCam.SetCamera(Camera.main);
                GameObject.Find("LeftTree").transform.position = new Vector3(-155.0f, 0.0f, 0.0f);
            }

        if (StartCamera)
        {
            switch (MoveCam.MoveCameraTime(StartCameraPos, CamEndPos, CameraPos, FocusTime, FocusDelTime, DelTime))
            {
                case 1:
                    BossName.gameObject.SetActive(true);
                    Debug.Log("‚È‚º“®‚­");
                    break;
                case 2:
                    {
                        BossBarName.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        BossHPBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        BossHPFrame.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        BossName.gameObject.SetActive(false);
                        StartCamera = false;
                        
                        Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_BATTLE;
                    }
                    break;
            }
        }
    }
}
