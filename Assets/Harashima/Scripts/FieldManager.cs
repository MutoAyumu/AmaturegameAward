using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    /// <summary>�X�^�[�g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnStart;

    /// <summary>���U���g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnClear;

    /// <summary>���U���g���ɌĂ΂�郁�\�b�h</summary>
    public event Action OnGameOver;

    void Start()
    {
        //�X�^�[�g�C�x���g���Ă�
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
        //�N���A�C�x���g���Ă�
        this?.OnClear();
    }
}
