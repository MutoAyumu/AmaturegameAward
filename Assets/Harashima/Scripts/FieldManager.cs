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

    void Start()
    {
        //スタートイベントを呼ぶ
        this?.OnStart();
    }

    void Update()
    {
        if (PlayerPalam.Instance?.Life <= 0)
        {
            this?.OnGameOver();
        }
    }

    public void Clear()
    {
        //クリアイベントを呼ぶ
        this?.OnClear();
    }
}
