using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrac : MonoBehaviour
{
    [SerializeField] Camera TracCamera;
    [SerializeField] float MaxLength;
    float Length;
    float HalfLength;
    float Dif;
    float CurrentDif;
    Vector3 A_Pos;
    Vector3 B_Pos;
    Vector3 CamPos;
    //作り方
    /*長さが一定以下の時は動かさず、長さが一定以上になったときに、元の長さの半分の所より大きいか小さいかでカメラの進む向きを変える。
      半分より小さいor大きい場合にはその差分をカメラで動かせばいい*/

    // Start is called before the first frame update
    void Start()
    {
        TracCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Difference();
        if (Length <= MaxLength)
        {
            return;
        }
        if (Length >= MaxLength)
        {
            if (HalfLength >= MaxLength / 2)
            {
                if (B_Pos.x >= A_Pos.x)
                {
                    Dif = HalfLength - (MaxLength / 2);
                    if (CurrentDif <= Dif)
                    {
                        //差分の座標
                        CamPos.x = TracCamera.transform.position.x + CurrentDif / Dif;
                        CamPos.y = TracCamera.transform.position.y;
                        CamPos.z = TracCamera.transform.position.z;

                        TracCamera.transform.position = CamPos;
                        CurrentDif++;
                        
                    }
                }
                if(B_Pos.x <= A_Pos.x)
                {
                    Dif = HalfLength - (MaxLength / 2);
                    if (CurrentDif <= Dif)
                    {
                        //差分の座標
                        CamPos.x = TracCamera.transform.position.x - CurrentDif / Dif;
                        CamPos.y = TracCamera.transform.position.y;
                        CamPos.z = TracCamera.transform.position.z;

                        TracCamera.transform.position = CamPos;
                        CurrentDif++;
                    }
                }
                
                Debug.Log("まったくまったく" + Dif);
                Debug.Log("熊って熊って" + CurrentDif);
            }
            //if (HalfLength <= MaxLength / 2)
            //{
            //    Dif = HalfLength - (MaxLength / 2);
            //    if (CurrentDif >= Dif)
            //    {
            //        //差分の座標
            //        CamPos.x = TracCamera.transform.position.x - CurrentDif / Dif;
            //        CamPos.y = TracCamera.transform.position.y;
            //        CamPos.z = TracCamera.transform.position.z;

            //        TracCamera.transform.position = CamPos;
            //        CurrentDif--;
            //    }
            //    Debug.Log("くまったくまった" + Dif);
            //}
        }
        
    }

    void Difference()
    {
        A_Pos = GameData.PlayerPos;
        B_Pos = Boss1Manager.BossPos;
        //Length = Mathf.Sqrt((A_Pos.x - B_Pos.x) * (A_Pos.x - B_Pos.x) + (A_Pos.y - B_Pos.y) * (A_Pos.y - B_Pos.y));
        Length = Vector2.Distance(A_Pos, B_Pos); ;
        HalfLength = Length / 2;
    }
}
