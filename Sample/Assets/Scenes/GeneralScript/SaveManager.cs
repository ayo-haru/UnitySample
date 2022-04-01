//=============================================================================
//
// �Z�[�u���[�h�}�l�[�W���[
//
// �쐬��:2022/03/16
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/15 ����
// 2022/03/28 �Z�[�u�̍��ڂ𑝂₵��
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       // UnityJson���g���̂ɕK�v
using System.IO;    // �t�@�C���A�N�Z�X�ɕK�v


[Serializable]  // ��������Ȃ��ƃV���A���C�Y�ł��Ȃ�
public struct SaveData {
    public List<string> deck;
    public int LastMapNumber;
    public Vector3 LastPlayerPos;
    public float HP;
}
/*
    �V���A���C�Y�Ƃ�
    ���s���̃A�v���P�[�V�����ɂ�����I�u�W�F�N�g�̒l�i��ԁj���A
    �o�C�i���`����XML�`���Ȃǂɕϊ����āA�ۑ����邱�Ƃ������B
    �V���A���C�Y���邱�Ƃɂ���āA�I�u�W�F�N�g���t�@�C���Ƃ���
    �i���I�ɕۑ�������ł���B
 */

public static class SaveManager {
    public static SaveData sd;
    const string SAVE_FILE_PATH = "save.json";  // �Z�[�u�f�[�^�̖��O

    public static void saveDeck(List<string> _deck) {
        sd.deck = _deck;
        save();
    }

    public static void saveLastMapNumber(int _CurrentMapNumber) {  // �}�b�v�̔ԍ���Json�ɕۑ�����
        sd.LastMapNumber = _CurrentMapNumber;
        save();
    }

    public static void saveLastPlayerPos(Vector3 _PlayerPos) {  // PlayerPos��Json�ɕۑ�����
        sd.LastPlayerPos = _PlayerPos;
        save();
    }

    public static void saveHP(float _HP) {  // HP��Json�ɕۑ�����
        sd.HP = _HP;
        save();
    }



    public static void save() {
        string json = JsonUtility.ToJson(sd);   //  Json�ɃV���A�A���C�Y
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();  // GetCurrentDirectory...�A�v���P�[�V�����̌��݂̃f�B���N�g�����擾
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        path += ("/" + SAVE_FILE_PATH); // �ۑ��ꏊ�̃p�X���i�[
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate,FileAccess.ReadWrite);
        StreamWriter writer = new StreamWriter(fs);    //  �㏑��
        writer.WriteLine(json); // ��s���������݂��ĉ��s
        writer.Flush();         // �o�b�t�@�Ɏc��l�����ׂď����o��
        writer.Close();         // �������݂̏I���ifclose()�݂����Ȃ�j
    }

    public static void load() {
        try
        {
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();  // GetCurrentDirectory...�A�v���P�[�V�����̌��݂̃f�B���N�g�����擾
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            FileInfo info = new FileInfo(path + "/" + SAVE_FILE_PATH);  // �ۑ��ꏊ����̃��[�h
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fs);    // info.OpenRead()�Ńt�@�C���p�X���Ƃ����ۂ�
            //StreamReader reader = new StreamReader(info.OpenRead());    // info.OpenRead()�Ńt�@�C���p�X���Ƃ����ۂ�
            string json = reader.ReadToEnd();                           // ReadToEnd�͈ꊇ�Ǎ��炵��ReadLine�ň�s���Ǎ�
            sd = JsonUtility.FromJson<SaveData>(json);                  // FromJson...Json��ǂݎ��C���X�^���X�̃f�[�^���㏑������
        }
        catch (Exception e)  //  ��O����
        {
            sd = new SaveData();
        }
    }
}

/*
 * Using���iDirectory�ł����Ă�j
 * �u�C���X�^���X���g�p��ɔj������v�Ƃ����ꍇ�ɗp������\��
 * Using�̌��()���ŃC���X�^���X���쐬���A{}���ł��̃C���X�^���X���g�����������L�q���Ă���
 * {}���̏��������s������A�����𔲂������ۂ�()�ō�����C���X�^���X�������I�ɔj������B
 * {}���ŃG���[���������Ă��A�K��()�ō��ꂽ�C���X�^���X���j�������
 */
