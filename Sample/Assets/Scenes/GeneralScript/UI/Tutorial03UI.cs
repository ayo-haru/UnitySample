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

    private Canvas canvas;  // �\������L�����o�X

    private int UIcnt;  // UI�\����

    private GameObject StarObj; // ���i�[

    private GameObject[] FragmentObj = new GameObject[3];   // ������i�[

    private bool onceMapOpen;

    // Start is called before the first frame update
    void Start()
    {
        UIcnt = 0;

        // �L�����o�X���w��
        canvas = GetComponent<Canvas>();

        //----- UI������ -----
        // ���ԉ�
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

        // �L�����o�X�̎q�Ɏw��
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

        // �����\�����Ȃ�
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

        //----- UI�\�������ɂ������I�u�W�F�N�g�̏����� -----
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
            //---������
            if (UIcnt == 0)
            {
                //---�����Ăނ��͂����Ă݂悤
                CharacterBack.GetComponent<ImageShow>().Show();
                Chara3_1.GetComponent<ImageShow>().Show();
                UIcnt++;
            }
            else if(UIcnt == 1 && !(StarObj))
            {
                //---HP����������܂���
                Chara3_1.GetComponent<ImageShow>().Clear();
                Chara3_2.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if(UIcnt == 2 && Chara3_2.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---�����͂�����HP����𑝂₷���Ƃ��ł��܂�
                Chara3_3.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
        }
        else if(GameData.PlayerPos.x >= -40.0f && UIcnt < 6)
        {
            //---���������
            if (UIcnt == 3 && Chara3_3.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---��������͂����Ă݂܂��傤
                Chara3_4.GetComponent<ImageShow>().Show();
                UIcnt++;
            }
            else if (UIcnt == 4 && (!FragmentObj[0] || !FragmentObj[1] || !FragmentObj[2]))
            {
                //---HP���񕜂��܂���
                Chara3_4.GetComponent<ImageShow>().Clear();
                Chara3_5.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 5 && Chara3_5.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---��������͂�����HP���񕜂��邱�Ƃ��ł��܂�
                Chara3_6.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }

        }
        else if (GameData.PlayerPos.x >= 125.0f && UIcnt < 10)
        {
            //---�}�b�v����
            if (UIcnt == 6 && Chara3_6.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---��������܂������Ă��Ēʂ�܂���
                Chara3_7.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 7 && Chara3_7.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---�ǂ����Ɏd�|�������肻���ł�
                Chara3_8.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 8 && Chara3_8.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---�}�b�v�Ŋm�F���Ă݂܂��傤
                Chara3_9.GetComponent<ImageShow>().Show(3);
                UIcnt++;
            }
            else if (UIcnt == 9 && Chara3_9.GetComponent<ImageShow>().mode == ImageShow.ImageMode.NONE)
            {
                //---�r���[�{�^���Ń}�b�v���J��

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

            //---�M�~�b�N����
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
            //---�Ƃ�����
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
