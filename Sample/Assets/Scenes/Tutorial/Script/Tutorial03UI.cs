using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial03UI : MonoBehaviour
{
    [SerializeField]
    private GameObject characterback;
    private GameObject CharacterBack;
    [SerializeField]
    private GameObject chara3_1;
    private GameObject Chara3_1;
    [SerializeField]
    private GameObject chara3_2;
    private GameObject Chara3_2;
    [SerializeField]
    private GameObject chara3_3;
    private GameObject Chara3_3;
    [SerializeField]
    private GameObject chara3_4;
    private GameObject Chara3_4;
    [SerializeField]
    private GameObject chara3_5;
    private GameObject Chara3_5;
    [SerializeField]
    private GameObject chara3_6;
    private GameObject Chara3_6;
    [SerializeField]
    private GameObject chara3_7;
    private GameObject Chara3_7;
    [SerializeField]
    private GameObject chara3_8;
    private GameObject Chara3_8;
    [SerializeField]
    private GameObject chara3_9;
    private GameObject Chara3_9;
    [SerializeField]
    private GameObject chara3_10;
    private GameObject Chara3_10;
    [SerializeField]
    private GameObject chara3_11;
    private GameObject Chara3_11;
    [SerializeField]
    private GameObject chara3_12;
    private GameObject Chara3_12;
    [SerializeField]
    private GameObject chara3_13;
    private GameObject Chara3_13;
    [SerializeField]
    private GameObject chara3_14;
    private GameObject Chara3_14;

    Canvas canvas;

    private int UIcnt;

    // Start is called before the first frame update
    void Start()
    {
        UIcnt = 0;

        // キャンバスを指定
        canvas = GetComponent<Canvas>();

        // 実態化
        CharacterBack = Instantiate(characterback);
        Chara3_1 = Instantiate(chara3_1);
        Chara3_2 = Instantiate(chara3_2);
        Chara3_3 = Instantiate(chara3_3);
        Chara3_4 = Instantiate(chara3_4);
        Chara3_5 = Instantiate(chara3_5);
        Chara3_6 = Instantiate(chara3_6);
        Chara3_7 = Instantiate(chara3_7);
        Chara3_8 = Instantiate(chara3_8);
        Chara3_9 = Instantiate(chara3_9);
        Chara3_10 = Instantiate(chara3_10);
        Chara3_11 = Instantiate(chara3_11);
        Chara3_12 = Instantiate(chara3_12);
        Chara3_13 = Instantiate(chara3_13);
        Chara3_14 = Instantiate(chara3_14);

        CharacterBack.transform.SetParent(this.canvas.transform, false);
        Chara3_1.transform.SetParent(this.canvas.transform, false);
        Chara3_2.transform.SetParent(this.canvas.transform, false);
        Chara3_3.transform.SetParent(this.canvas.transform, false);
        Chara3_4.transform.SetParent(this.canvas.transform, false);
        Chara3_5.transform.SetParent(this.canvas.transform, false);
        Chara3_6.transform.SetParent(this.canvas.transform, false);
        Chara3_7.transform.SetParent(this.canvas.transform, false);
        Chara3_8.transform.SetParent(this.canvas.transform, false);
        Chara3_9.transform.SetParent(this.canvas.transform, false);
        Chara3_10.transform.SetParent(this.canvas.transform, false);
        Chara3_11.transform.SetParent(this.canvas.transform, false);
        Chara3_12.transform.SetParent(this.canvas.transform, false);
        Chara3_13.transform.SetParent(this.canvas.transform, false);
        Chara3_14.transform.SetParent(this.canvas.transform, false);

        CharacterBack.GetComponent<ImageShow>().Hide();
        Chara3_1.GetComponent<ImageShow>().Hide();
        Chara3_2.GetComponent<ImageShow>().Hide();
        Chara3_3.GetComponent<ImageShow>().Hide();
        Chara3_4.GetComponent<ImageShow>().Hide();
        Chara3_5.GetComponent<ImageShow>().Hide();
        Chara3_6.GetComponent<ImageShow>().Hide();
        Chara3_7.GetComponent<ImageShow>().Hide();
        Chara3_8.GetComponent<ImageShow>().Hide();
        Chara3_9.GetComponent<ImageShow>().Hide();
        Chara3_10.GetComponent<ImageShow>().Hide();
        Chara3_11.GetComponent<ImageShow>().Hide();
        Chara3_12.GetComponent<ImageShow>().Hide();
        Chara3_13.GetComponent<ImageShow>().Hide();
        Chara3_14.GetComponent<ImageShow>().Hide();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.PlayerPos.x >= -200.0f && UIcnt == 0)
        {
            CharacterBack.GetComponent<ImageShow>().Show();
            Chara3_1.GetComponent<ImageShow>().Show(120);
            UIcnt++;
        }

    }
}
