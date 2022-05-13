using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEntry : MonoBehaviour
{
    Vector3 StartCameraPos;
    Vector3 CamEndPos;
    CameraMove MoveCam;
    [SerializeField] public Image BossName;
    [SerializeField] public Image BossBarName;
    [SerializeField] public Image BossHPFrame;
    [SerializeField] public Image BossHPBar;
    [SerializeField] float DelTime;
    [SerializeField] float FocusTime;
    [SerializeField] float FocusDelTime;
    bool ReturnCamera;
    // Start is called before the first frame update
    void Start()
    {
        MoveCam = GetComponent<CameraMove>();
        StartCameraPos = Camera.main.transform.position;
        CamEndPos = Boss1Manager.BossPos;
        CamEndPos.z = StartCameraPos.z;
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
        MoveCam.SetCamera(Camera.main);
        switch (MoveCam.MoveCameraTime(StartCameraPos, CamEndPos, FocusTime, FocusDelTime, DelTime))
        {
            case 1:
                BossName.gameObject.SetActive(true);
                break;
            case 2:
                {
                    BossBarName.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BossHPBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BossHPFrame.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    BossName.gameObject.SetActive(false);
                    Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_BATTLE;
                }
                break;
        }
    }
}
