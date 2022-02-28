using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    /// <summary>スタート時に呼ばれるメソッド</summary>
    public event Action OnStart;

    /// <summary>リザルト時に呼ばれるメソッド</summary>
    public event Action OnClear;

    /// <summary>リザルト時に呼ばれるメソッド</summary>
    public event Action OnGameOver;

    /// <summary>クリアかゲームオーバーを判定するフラグ</summary>
    bool isEnd = false;

    [SerializeField, Tooltip("ステージの番号")]
    int _stageIndex;

    void Start()
    {
        //テスト用
        OnGameOver += DebugGameOver;
        OnStart += DebugStart;
        OnClear += DebugClear;
        //スタートイベントを呼ぶ
        if (OnStart != null)
        {
            OnStart();
        }        
    }


    void Update()
    {
        if (PlayerPalam.Instance?.Life <= 0 && !isEnd)　//スコアが0になり、isEndがFalseだったら
        {
            if(OnGameOver != null)
            {
                //ゲームオーバーイベントを呼ぶ
                OnGameOver();
            }            
            isEnd = true;
        }
    }


    public void Clear()
    {
        //クリアイベントを呼ぶ
        if(OnClear!= null && !isEnd)//isEndがFalseだったら
        {
            OnClear();
            isEnd = true;
        }
    }
    void DebugGameOver()
    {
        Debug.Log("ゲームオーバー");
    }
    void DebugStart()
    {
        Debug.Log("スタート");
    }
    void DebugClear()
    {
        Debug.Log("クリア");
    }
}
