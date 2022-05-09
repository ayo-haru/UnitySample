using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject gate_Open;
    private MeshRenderer Mesh_Open;
    private GameObject gate_Close;
    private MeshRenderer Mesh_Close;

    // Start is called before the first frame update
    void Start()
    {
        gate_Close = GameObject.Find("goal_close");
        Mesh_Close = gate_Close.GetComponent<MeshRenderer>();
        gate_Open = GameObject.Find("goal_open");
        Mesh_Open = gate_Open.GetComponent<MeshRenderer>();

        if (GameData.GateOnOff)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open() {
        Mesh_Close.enabled = false;
        Mesh_Open.enabled = true;
        gate_Open.transform.parent.GetComponent<BoxCollider>().enabled = true;
        gate_Open.transform.parent.GetComponent<BoxCollider>().isTrigger = true;
    }
    public void Close() {
        Mesh_Close.enabled = true;
        Mesh_Open.enabled = false;
        gate_Open.transform.parent.GetComponent<BoxCollider>().enabled = false;
        gate_Open.transform.parent.GetComponent<BoxCollider>().isTrigger = false;

    }
}
