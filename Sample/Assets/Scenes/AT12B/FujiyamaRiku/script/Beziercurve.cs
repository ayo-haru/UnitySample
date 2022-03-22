using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beziercurve : MonoBehaviour
{
    static private Vector3 CurvePos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    static public Vector3 SecondCurve(Vector3 StartPoint, Vector3 MiddlePoint, Vector3 EndPoint, float Time)
    {
        Vector3 S = Vector3.Lerp(StartPoint, MiddlePoint, Time);
        Vector3 E = Vector3.Lerp(MiddlePoint, EndPoint, Time);
        CurvePos = Vector3.Lerp(S, E, Time);

        return CurvePos;
    }
    static public Vector3 ThirdCurve(Vector3 StartPoint, Vector3 FirstMiddlePoint,
                                     Vector3 SecondMiddlePoint, Vector3 EndPoint, float Time)
    {
        Vector3 FS = Vector3.Lerp(StartPoint, FirstMiddlePoint, Time);
        Vector3 FM = Vector3.Lerp(FirstMiddlePoint, SecondMiddlePoint, Time);
        Vector3 FE = Vector3.Lerp(SecondMiddlePoint, EndPoint, Time);

        Vector3 SS = Vector3.Lerp(FS, FM, Time);
        Vector3 SE = Vector3.Lerp(FM, FE, Time);

        Vector3 End = Vector3.Lerp(SS, SE, Time);

        return CurvePos;
    }
}
