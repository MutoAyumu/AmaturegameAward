using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    /// <summary>スタート時に呼ばれるメソッド</summary>
    public event Action OnStart;

    /// <summary>リザルト時に呼ばれるメソッド</summary>
    public event Action OnResult;

    void Start()
    {
        //メソッドを呼ぶ
        if(OnStart !=null)
        {
            OnStart();
        }        
    }

    void Update()
    {
        if(PlayerPalam.Instance?.Life <= 0)
        {
            OnResult();
        }
    }
}
