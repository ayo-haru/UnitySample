//=============================================================================
//
// HP�Ǘ�
//
// �쐬��:2022/03/25
// �쐬��:�g����
//
// <�J������>
// 2022/03/25 �쐬
// 2022/03/28 ������
// 2022/04/19 �A�C�e���擾���̏����ǉ�
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
   // public�@Image HP;

   public int MaxHP = 5;                          // HP�̍ő�l

    public GameObject PieceManager;                 //������}�l�[�W���[
    public GameObject HPImage;                      //�����摜

    private PieceManager pieceManager;          //������}�l�[�W���[�R���|�[�l���g
    private UVScroll hpTex;                     //������UVScroll�R���|�[�l���g

    private void Awake()
    {
        //---�J�n���ɕ\������UI(GameObject)�̃A�N�e�B�u��Ԃ�ݒ� true = �L�� / false = ����
        //GameObject.Find("Full Moon").SetActive(true);
        //GameObject.Find("Harf Moon1").SetActive(false);
        //GameObject.Find("Harf Moon2").SetActive(false);
        //GameObject.Find("Harf Moon3").SetActive(false);
        //GameObject.Find("New Moon").SetActive(false);

        pieceManager = PieceManager.GetComponent<PieceManager>();
        hpTex = HPImage.GetComponent<UVScroll>();


    }
    // Start is called before the first frame update
    void Start()
    {
        //���݂̂��������ƂɃe�N�X�`�����W�ݒ�
        hpTex.SetFrame(GameData.CurrentHP);
       // HP.fillAmount = (float)GameData.CurrentHP / MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
      //  HP.fillAmount = (float)GameData.CurrentHP / MaxHP;
        //Debug.Log("HP��:"+HP.fillAmount);
        //Debug.Log("MAXHP:"+MaxHP);
        //Debug.Log("�cHP:"+GameData.CurrentHP);
    }

    public void GetPiece()
    {
        //HP�������Ă�����HP��1��
        if(GameData.CurrentHP < MaxHP)
        {
            ++GameData.CurrentHP;
            hpTex.SetFrame(GameData.CurrentHP);
        }
        else//HP�����^���������炩����𑝂₷
        {
            pieceManager.GetPiece();
        }
    }

    public void Damaged()
    {
        if (!pieceManager.DelPiece())
        {
            //�����炪�Ȃ�������HP���炷
            --GameData.CurrentHP;
            hpTex.SetFrame(GameData.CurrentHP);
        }
    }

    public void GetItem()
    {
        pieceManager.GetItem();
    }
}
