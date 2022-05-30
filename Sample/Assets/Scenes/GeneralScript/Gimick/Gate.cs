using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject gate_Open;
    private MeshRenderer Mesh_Open;
    private GameObject gate_Close;
    private MeshRenderer Mesh_Close;

    private GameObject gate;
    public Animator animator;
    private bool oldGateflg;
    private ShakeCamera shakecamera;
    // Start is called before the first frame update
    void Start()
    {
        oldGateflg = GameData.GateOnOff;
        //gate_Close = GameObject.Find("Gate_Close");
        //Mesh_Close = gate_Close.GetComponent<MeshRenderer>();
        //gate_Open = GameObject.Find("Gate_Open");
        //Mesh_Open = gate_Open.GetComponent<MeshRenderer>();
        gate = GameObject.Find("GateToStage1");
        if (!gate)
        {
            gate = GameObject.Find("GateToBoss1");
        }

        shakecamera = GetComponent<ShakeCamera>();

        if (GameData.GateOnOff)
        {
            //Close();
            gate.GetComponent<BoxCollider>().enabled = false;
            gate.GetComponent<BoxCollider>().isTrigger = false;

        }
        else
        {
            Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(oldGateflg != GameData.GateOnOff)
        {
            shakecamera.Shake(0.5f, 1000, 5);
        }
        oldGateflg = GameData.GateOnOff;
    }

    public void Open() {
        //Mesh_Close.enabled = false;
        //Mesh_Open.enabled = true;
        //gate_Open.GetComponent<BoxCollider>().enabled = true;
        //gate_Open.GetComponent<BoxCollider>().isTrigger = true;
        gate.GetComponent<BoxCollider>().enabled = true;
        gate.GetComponent<BoxCollider>().isTrigger = true;

        animator.Play("Open");
        //shakecamera.Shake(0.5f, 1000, 5);
    }
    public void Close() {
        //Mesh_Close.enabled = true;
        //Mesh_Open.enabled = false;
        //gate_Open.GetComponent<BoxCollider>().enabled = false;
        //gate_Open.GetComponent<BoxCollider>().isTrigger = false;
        gate.GetComponent<BoxCollider>().enabled = false;
        gate.GetComponent<BoxCollider>().isTrigger = false;
        animator.Play("Close");
        //shakecamera.Shake(0.5f, 1000, 5);
    }
}
