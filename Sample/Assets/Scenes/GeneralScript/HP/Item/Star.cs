using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // HP�}�l�[�W���[
    GameObject hp;                                      // HP�̃I�u�W�F�N�g���i�[
    HPManager hpmanager;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("HPSystem(2)(Clone)"))
        {
            hp = GameObject.Find("HPSystem(2)(Clone)");         // HPSystem���Q��
            hpmanager = hp.GetComponent<HPManager>();           // HPSystem�̎g�p����R���|�[�l���g
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Weapon")   // ���Ɠ���������UI��ύX���ĉ������ăG�t�F�N�g�����ď���
        {
            if (GameObject.Find("HPSystem(2)(Clone)"))
            {

                hpmanager.GetItem();
            }
            Destroy(gameObject);
            SoundManager.Play(SoundData.eSE.SE_REFLECTION_STAR, SoundData.GameAudioList);
            Vector3 effekctPos = this.transform.position;
            //effekctPos.y -= 2.5f;
            EffectManager.Play(EffectData.eEFFECT.EF_HEALITEM, effekctPos);
            //Debug.Log("�����Ƃ����Ă�");
        }
    }
}
