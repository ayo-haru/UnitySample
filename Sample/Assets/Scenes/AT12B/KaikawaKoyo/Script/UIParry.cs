//==========================================================
//      UI�e�������̏���
//      �쐬���@2022/05/08
//      �쐬�ҁ@�C��W�k
//      
//      <�J������>
//      2022/05/08
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIParry : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody rb;
    private Vector3 vec;

    public bool isAlive;                    
    private float Timer;                    // ���Ԋi�[�p
    private float DestroyTime = 0.0f;       // ������܂ł̎���
    private float bouncePower = 200.0f;     // ������ԋ���

    //---�q�b�g�X�g�b�v���o(2022/05/02.�g��)
    Player2 player2;
    public float Width = 0.1f;
    public int RoundCnt = 4;
    public float Duration = 0.23f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        isAlive = true;
        rb = gameObject.GetComponent<Rigidbody>();
        player2 = Player.GetComponent<Player2>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ԃŏ����鏈��
        if (!isAlive)
        {
            DestroyTime += Time.deltaTime;
        }
        if (DestroyTime > 1.5f)
        {
            Destroy(gameObject, 0.0f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // �e���ꂽ��x�N�g�����v�Z���Ċ֐����Ăяo��
        if (collision.gameObject.name == "Weapon(Clone)" && isAlive)
        {
            vec = (transform.position - Player.transform.position).normalized;

            //---�q�b�g�X�g�b�v���o
            var seq = DOTween.Sequence();
            //---UI�̐U�����o
            seq.Append(transform.DOShakePosition(player2.HitStopTime, 1f, 100, fadeOut: false));

            //---���̃^�C�~���O��UI���΂��������Ăяo��
            seq.AppendCallback(() => UIDestroy(vec, Player.transform.position.x));
            Shake(0.1f, 5, 0.23f);
        }
    }

    // UI���΂�����
    public void UIDestroy(Vector3 vec, float x)
    {
        //�v���C���[�Ƌt�����ɒ��˕Ԃ�
        rb.velocity = vec * bouncePower;

        // ��O�ɐ�����΂�
        //rb.velocity = new Vector3(0.0f, 0.0f, -bouncePower);

        // ��]������
        if (x < transform.position.x)
        {
            rb.angularVelocity = new Vector3(0.0f, 0.0f, -500.0f);
        }
        if (x > transform.position.x)
        {
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 500.0f);
        }

        SoundManager.Play(SoundData.eSE.SE_REFLECTION, SoundData.GameAudioList);

        isAlive = false;
    }
    /// <summary>
    /// �J�����U�����o
    /// </summary>
    /// <param name="width"></param>    �J�����̐U�ꕝ
    /// <param name="cnt"></param>      ������
    /// <param name="duration"></param> ����
    public void Shake(float width, int cnt, float duration)
    {
        var camera = Camera.main.transform;
        var seq = DOTween.Sequence();

        var partDuration = duration / cnt / 2f;

        var widthHalf = width / 2f;

        for (int i = 0; i < cnt - 1; i++)
        {
            seq.Append(camera.DOLocalRotate(new Vector3(-width, 0f), partDuration));
            seq.Append(camera.DOLocalRotate(new Vector3(width, 0f), partDuration));
        }

        seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf, 0f), partDuration));
        seq.Append(camera.DOLocalRotate(Vector3.zero, partDuration));
    }
}
