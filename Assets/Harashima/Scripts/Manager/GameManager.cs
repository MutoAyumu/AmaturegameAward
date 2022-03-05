using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(1, 10), Tooltip("何ステージあるか")]
    int _stageLimit;

    /// <summary>現在のステージクリア状況</summary>
    bool[] _clearedStage;


    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //ステージの数で初期化
        _clearedStage = new bool[_stageLimit];
    }


    /// <summary>
    /// ステージをクリアした際の処理を行う関数
    /// </summary>
    /// <param name="index">1以上のステージ数以下の値</param>
    public void ClearStage(int index)
    {
        int num = index - 1;
        num = Mathf.Clamp(num,0,_stageLimit-1);
        _clearedStage[num] = true;
    }
}
