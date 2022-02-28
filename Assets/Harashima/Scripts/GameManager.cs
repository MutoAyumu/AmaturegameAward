using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Range(1, 10), Tooltip("���X�e�[�W���邩")]
    int _stageLimit;

    /// <summary>���݂̃X�e�[�W�N���A��</summary>
    bool[] _clearedStage;


    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //�X�e�[�W�̐��ŏ�����
        _clearedStage = new bool[_stageLimit];
    }


    /// <summary>
    /// �X�e�[�W���N���A�����ۂ̏������s���֐�
    /// </summary>
    /// <param name="index">1�ȏ�̃X�e�[�W���ȉ��̒l</param>
    public void ClearStage(int index)
    {
        int num = index - 1;
        num = Mathf.Clamp(num,0,_stageLimit-1);
        _clearedStage[num] = true;
    }
}
