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

    private Canvas canvas;  // 表示するキャンバス

    private int UIcnt;  // UI表示順

    private GameObject StarObj; // 星格納

    private GameObject[] FragmentObj = new GameObject[3];   // かけら格納

    private bool onceMapOpen;

    // Start is called before the first frame update
    void Start()
    {
        UIcnt = 0;

        // キャンバスを指定
        canvas = GetComponent<Canvas>();

        //----- UI初期化 -----
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

        // キャンバスの子に指定
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

        // 何も表示しない
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

        //----- UI表示条件にかかわるオブジェクトの初期化 -----
        StarObj = GameObject.Find("ItemStar");
        FragmentObj[0] = GameObject.Find("Star_Fragment000");
        FragmentObj[1] = GameObject.Find("Star_Fragment001");
        FragmentObj[2] = GameObject.Find("Star_Fragment002");


    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.PlayerPos.x >= -80.0f && UIcnt < 3)
        {
            //---星説明
            if (UIcnt == 0)
            {
                //---あいてむをはじいてみよう
                CharacterBack.GetComponent<ImageShow>().Show();
                Chara3_1.GetComponent<ImageShow>().Show();
                UIcnt++;
            }
            else if(UIcnt == 1 && !(StarObj))
            {
                //---HP上限が増えました
                Chara3_1.GetComponent<ImageShow>().Clear();
                Chara3_2.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if(UIcnt == 2 && Chara3_2.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---星をはじくとHP上限を増やすことができます
                Chara3_3.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
        }
        else if(GameData.PlayerPos.x >= -40.0f && UIcnt < 6)
        {
            //---かけら説明
            if (UIcnt == 3 && Chara3_3.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---かけらをはじいてみましょう
                Chara3_4.GetComponent<ImageShow>().Show();
                UIcnt++;
            }
            else if (UIcnt == 4 && (!FragmentObj[0] || !FragmentObj[1] || !FragmentObj[2]))
            {
                //---HPが回復しました
                Chara3_4.GetComponent<ImageShow>().Clear();
                Chara3_5.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 5 && Chara3_5.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---かけらをはじくとHPを回復することができます
                Chara3_6.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }

        }
        else if (GameData.PlayerPos.x >= 125.0f && UIcnt < 10)
        {
            //---マップ説明
            if (UIcnt == 6 && Chara3_6.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---扉がありますが閉じていて通れません
                Chara3_7.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 7 && Chara3_7.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---どこかに仕掛けがありそうです
                Chara3_8.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 8 && Chara3_8.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---マップで確認してみましょう
                Chara3_9.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 9 && Chara3_9.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---ビューボタンでマップを開く

                Chara3_10.GetComponent<ImageShow>().Show();
                UIcnt++;
            }
        }
        else if (GameData.PlayerPos.x >= 370.0f && GameData.PlayerPos.y >= 90.0f && UIcnt < 14)
        {
            if(GameData.GateOnOff == false)
            {
                UIcnt = 11;
            }

            //---ギミック説明
            if (UIcnt == 10)
            {
                Chara3_10.GetComponent<ImageShow>().Clear();
                Chara3_11.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 11 && Chara3_11.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                Chara3_12.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
        }
        else if(GameData.PlayerPos.x <= 275.0f && UIcnt == 12)
        {
            //---とおれるよ
            Chara3_12.GetComponent<ImageShow>().Clear();
            Chara3_13.GetComponent<ImageShow>().Show(3);

            UIcnt++;
        }
        else if(UIcnt == 13 && Chara3_13.GetComponent<ImageShow>().mode == ImageShow.ImageMode.HIDE)
        {
            CharacterBack.GetComponent<ImageShow>().FadeOut();
        }

    }
}
