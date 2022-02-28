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

    /// <summary>�N���A���Q�[���I�[�o�[�𔻒肷��t���O</summary>
    bool isEnd = false;

    [SerializeField, Tooltip("�X�e�[�W�̔ԍ�")]
    int _stageIndex;

    void Start()
    {
        //�e�X�g�p
        OnGameOver += DebugGameOver;
        OnStart += DebugStart;
        OnClear += DebugClear;
        //�X�^�[�g�C�x���g���Ă�
        if (OnStart != null)
        {
            OnStart();
        }        
    }


    void Update()
    {
        if (PlayerPalam.Instance?.Life <= 0 && !isEnd)�@//�X�R�A��0�ɂȂ�AisEnd��False��������
        {
            if(OnGameOver != null)
            {
                //�Q�[���I�[�o�[�C�x���g���Ă�
                OnGameOver();
            }            
            isEnd = true;
        }
    }


    public void Clear()
    {
        //�N���A�C�x���g���Ă�
        if(OnClear!= null && !isEnd)//isEnd��False��������
        {
            OnClear();
            isEnd = true;
        }
    }
    void DebugGameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
    }
    void DebugStart()
    {
        Debug.Log("�X�^�[�g");
    }
    void DebugClear()
    {
        Debug.Log("�N���A");
    }
}
