using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPalam : Singleton<PlayerPalam>
{
    [SerializeField, Tooltip("���C�t�̏����l")]
    int _initialLife = 3;

    /// <summary>���݂̃��C�t</summary>
    int _life;
    /// <summary>���C�t�̓ǂݎ��v���p�e�B</summary>
    public int Life => _life;

    /// <summary>
    /// ���݂̃��C�t��ύX����֐�
    /// </summary>
    /// <param name="value">���₷�Ȃ琳�A���炷�Ȃ畉</param>
    public void LifeChange(int value)
    {
        Debug.Log($"�ω��O�F{_life}");
        if (_life + value <= 0)
        {
            _life = 0;
        }
        else
        {
            _life += value;
        }
        Debug.Log($"�ω���F{_life}");
    }

    protected override void OnAwake()
    {
        //�V�[�����؂�ւ���Ă��l���ێ������悤��
        DontDestroyOnLoad(this);

        //���C�t��������
        _life = _initialLife;
    }

}
