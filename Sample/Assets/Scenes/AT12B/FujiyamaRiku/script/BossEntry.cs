using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEntry : MonoBehaviour
{
    Vector3 StartCameraPos;
    Vector3 CamEndPos;
    [SerializeField] public Image BossName;
    [SerializeField] public Image BossBarName;
    [SerializeField] public Image BossHPFrame;
    [SerializeField] public Image BossHPBar;
    [SerializeField] float DelSpeed;
    float FocusTime;
    float FocusDelTime;
    bool ReturnCamera;
    // Start is called before the first frame update
    void Start()
    {
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
        Camera camera = Camera.main;
        if (!ReturnCamera)
        {
            FocusTime += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(StartCameraPos, CamEndPos, FocusTime);
            if (FocusTime >= 1.0f)
            {
                BossName.gameObject.SetActive(true);
                FocusDelTime += Time.deltaTime * DelSpeed;
                if (FocusDelTime >= 1.0f)
                {
                    FocusTime = 0.0f;
                    FocusDelTime = 0.0f;
                    ReturnCamera = true;
                }
            }
        }
        if(ReturnCamera)
        {
            FocusTime += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(CamEndPos, StartCameraPos, FocusTime);
            if (FocusTime >= 1.0f)
            {
                BossBarName.color = new Color(1.0f,1.0f,1.0f,1.0f);
                BossHPBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BossHPFrame.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BossName.gameObject.SetActive(false);
                FocusTime = 0.0f;
                ReturnCamera = false;
                Boss1Manager.BossState = Boss1Manager.Boss1State.BOSS1_BATTLE;
            }
        }
    }
}
