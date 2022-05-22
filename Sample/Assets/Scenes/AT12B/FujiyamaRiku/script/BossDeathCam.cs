using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathCam : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 EndPos;
    CameraMove MoveCamera;
    BossEntry BossImage;
    [SerializeField] float Time;

    // Start is called before the first frame update
    void Start()
    {
        MoveCamera = GetComponent<CameraMove>();
        BossImage = GetComponent<BossEntry>();
        EndPos = Camera.main.transform.position;
        MoveCamera.SetCamera(Camera.main);

    }

    // Update is called once per frame
    void Update()
    {
        StartPos = Boss1Manager.BossPos;
        StartPos.z = EndPos.z + 50.0f;
    }
    public void DeathCamera()
    {
        if (MoveCamera.MoveCameraTime(StartPos, EndPos, Time))
        {
            BossImage.BossBarName.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            BossImage.BossHPBar.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            BossImage. BossHPFrame.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
    
}
