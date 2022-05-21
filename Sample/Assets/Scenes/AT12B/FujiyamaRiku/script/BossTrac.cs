using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrac : MonoBehaviour
{
    [SerializeField] Camera TracCamera;
    [SerializeField] float MaxLength;
    [SerializeField] float UpMax;
    float LimitLength;
    float LimitUp;
    float Length;
    float UpLength;
    float HalfLength;
    float UpHalf;
    float Dif;
    float CurrentDif;
    float UpDif;
    float UpCurrentDif;
    bool ChangeFlg;
    Vector3 A_Pos;
    Vector3 B_Pos;
    Vector3 CamPos;
    Vector3 BegCamPos;
    //����
    /*���������ȉ��̎��͓��������A���������ȏ�ɂȂ����Ƃ��ɁA���̒����̔����̏����傫�������������ŃJ�����̐i�ތ�����ς���B
      ������菬����or�傫���ꍇ�ɂ͂��̍������J�����œ������΂���*/

    // Start is called before the first frame update
    void Start()
    {
        TracCamera = Camera.main;
        BegCamPos = GameObject.Find("CameraPos").transform.position;
        LimitLength = MaxLength + 25;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("�܂������܂�����" + CurrentDif);
        Debug.Log("�F���ČF����" + UpLength);
        Difference();
        if(UpLength <= UpMax)
        {
            UpDif = UpHalf - (UpMax / 2);
            if (UpCurrentDif > 0)
            {
                if (CamPos.y > BegCamPos.y)
                {
                    CamPos.x = TracCamera.transform.position.x;
                    CamPos.y = TracCamera.transform.position.y - 1.0f;
                    CamPos.z = TracCamera.transform.position.z;

                    TracCamera.transform.position = CamPos;
                }
                UpCurrentDif--;
            }
            else
            {
                UpCurrentDif = 0;
            }
        }
        if(UpLength >= UpMax)
        {
            UpDif = UpHalf - (UpMax / 2);
            if(UpCurrentDif <= UpDif)
            {
                CamPos.x = TracCamera.transform.position.x;
                CamPos.y = TracCamera.transform.position.y + UpCurrentDif / UpDif;
                CamPos.z = TracCamera.transform.position.z;

                TracCamera.transform.position = CamPos;
                UpCurrentDif++;
            }
        }
        if (Length >= MaxLength && Length <= LimitLength)
        {
            Debug.Log("�͂����Ă邺");
                if (B_Pos.x >= A_Pos.x)
                {
                if(ChangeFlg)
                {
                    CurrentDif = 0;
                    ChangeFlg = false;
                }
                    Dif = HalfLength - (MaxLength / 2);
                    if (CurrentDif == Dif)
                    {
                        return;
                    }
                    if (CurrentDif <= Dif)
                    {
                        //�����̍��W
                        CamPos.x = TracCamera.transform.position.x + CurrentDif / Dif;
                        CamPos.y = TracCamera.transform.position.y;
                        CamPos.z = TracCamera.transform.position.z;

                        TracCamera.transform.position = CamPos;
                        CurrentDif++;
                        return;
                    }
                }
                if(B_Pos.x <= A_Pos.x)
                {
                if (!ChangeFlg)
                {
                    CurrentDif = 0;
                    ChangeFlg = true;
                }
                Dif = HalfLength - (MaxLength / 2);
                if (CurrentDif == Dif)
                {
                    return;
                }
                if (CurrentDif <= Dif)
                    {
                        //�����̍��W
                        CamPos.x = TracCamera.transform.position.x - CurrentDif / Dif;
                        CamPos.y = TracCamera.transform.position.y;
                        CamPos.z = TracCamera.transform.position.z;

                        TracCamera.transform.position = CamPos;
                        CurrentDif++;
                        return;
                    }
            }
        }
        
    }

    void Difference()
    {
        A_Pos = GameData.PlayerPos;
        B_Pos = Boss1Manager.BossPos;
        Length = Mathf.Sqrt((A_Pos.x - B_Pos.x) * (A_Pos.x - B_Pos.x) + (A_Pos.y - B_Pos.y) * (A_Pos.y - B_Pos.y));
        UpLength = B_Pos.y - A_Pos.y;
        //Length = Vector2.Distance(A_Pos, B_Pos); ;
        HalfLength = Length / 2;
        UpHalf = UpLength / 2;
    }
}
