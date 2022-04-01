//=============================================================================
//
// セーブロードマネージャー
//
// 作成日:2022/03/16
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/15 実装
// 2022/03/28 セーブの項目を増やした
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       // UnityJsonを使うのに必要
using System.IO;    // ファイルアクセスに必要


[Serializable]  // これをつけないとシリアライズできない
public struct SaveData {
    public List<string> deck;
    public int LastMapNumber;
    public Vector3 LastPlayerPos;
    public float HP;
}
/*
    シリアライズとは
    実行中のアプリケーションにおけるオブジェクトの値（状態）を、
    バイナリ形式やXML形式などに変換して、保存することをいう。
    シリアライズすることによって、オブジェクトをファイルとして
    永続的に保存したりできる。
 */

public static class SaveManager {
    public static SaveData sd;
    const string SAVE_FILE_PATH = "save.json";  // セーブデータの名前

    public static void saveDeck(List<string> _deck) {
        sd.deck = _deck;
        save();
    }

    public static void saveLastMapNumber(int _CurrentMapNumber) {  // マップの番号をJsonに保存する
        sd.LastMapNumber = _CurrentMapNumber;
        save();
    }

    public static void saveLastPlayerPos(Vector3 _PlayerPos) {  // PlayerPosをJsonに保存する
        sd.LastPlayerPos = _PlayerPos;
        save();
    }

    public static void saveHP(float _HP) {  // HPをJsonに保存する
        sd.HP = _HP;
        save();
    }



    public static void save() {
        string json = JsonUtility.ToJson(sd);   //  Jsonにシリアアライズ
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();  // GetCurrentDirectory...アプリケーションの現在のディレクトリを取得
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        path += ("/" + SAVE_FILE_PATH); // 保存場所のパスを格納
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate,FileAccess.ReadWrite);
        StreamWriter writer = new StreamWriter(fs);    //  上書き
        writer.WriteLine(json); // 一行ずつ書き込みして改行
        writer.Flush();         // バッファに残る値をすべて書き出す
        writer.Close();         // 書き込みの終了（fclose()みたいなやつ）
    }

    public static void load() {
        try
        {
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();  // GetCurrentDirectory...アプリケーションの現在のディレクトリを取得
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            FileInfo info = new FileInfo(path + "/" + SAVE_FILE_PATH);  // 保存場所からのロード
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(fs);    // info.OpenRead()でファイルパスがとれるっぽい
            //StreamReader reader = new StreamReader(info.OpenRead());    // info.OpenRead()でファイルパスがとれるっぽい
            string json = reader.ReadToEnd();                           // ReadToEndは一括読込らしいReadLineで一行ずつ読込
            sd = JsonUtility.FromJson<SaveData>(json);                  // FromJson...Jsonを読み取りインスタンスのデータを上書きする
        }
        catch (Exception e)  //  例外処理
        {
            sd = new SaveData();
        }
    }
}

/*
 * Using文（Directoryでつかわれてる）
 * 「インスタンスを使用後に破棄する」という場合に用いられる構文
 * Usingの後の()内でインスタンスを作成し、{}内でそのインスタンスを使った処理を記述していく
 * {}内の処理を実行した後、そこを抜けだす際に()で作ったインスタンスを自動的に破棄する。
 * {}内でエラーが発生しても、必ず()で作られたインスタンスが破棄される
 */
