using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    public float speed = 0.5f;

    [System.NonSerialized]
    public bool isBlink = false;
    [System.NonSerialized]
    public bool isHide = false;
    private Image UI;
    private float time;
    private float red, green, blue, alfa;   //パネルの色、不透明度を管理


    // Start is called before the first frame update
    void Start()
    {
         UI = this.gameObject.GetComponent<Image>();
        red = UI.color.r;
        green = UI.color.g;
        blue = UI.color.b;
        alfa = UI.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        if (isBlink)
        {
            Blink();
        }
        else
        {
            if (isHide)
            {
                Hide();
                return;
            }
            BlinkReset();
        }
    }

    void Blink()
    {
        float alfaCorrection = 0.3f;
        time += Time.deltaTime * 5.0f * speed;
        alfa = Mathf.Sin(time) * 0.5f + 0.5f;
        alfa += alfaCorrection;
        UI.color = new Color(red, green, blue, alfa);
    }

    void BlinkReset() {
        alfa = 1.0f;
        UI.color = new Color(red, green, blue, alfa);

    }

    void Hide() {
        alfa = 0.0f;
        UI.color = new Color(red, green, blue, alfa);

    }
}
