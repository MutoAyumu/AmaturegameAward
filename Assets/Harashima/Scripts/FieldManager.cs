using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    /// <summary>�X�^�[�g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnStart;

    /// <summary>���U���g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnResult;

    void Start()
    {
        //���\�b�h���Ă�
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
