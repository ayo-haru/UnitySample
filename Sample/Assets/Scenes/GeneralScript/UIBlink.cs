using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    public float speed = 0.1f;

    [System.NonSerialized]
    //public Ç±Ç±Ç≈óÕêsÇ´Ç∆ÇÈÇ≈Ç†ÇÒÇΩ
    private Image UI;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
         UI = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Color GetAlphaColor(Color color) {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
