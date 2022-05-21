using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃X�e�[�^�X���Ǘ�����N���X
/// </summary>
public class PlayerPalam : Singleton<PlayerPalam>
{
    [Header("���C�t")]
    [SerializeField, Range(1, 10), Tooltip("���C�t�̏����l")]
    int _initialLife = 3;
    [SerializeField, Range(1, 10), Tooltip("���C�t�̍ő�l")]
    int _lifeLimit = 5;

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
        int last = _life;
        _life = Mathf.Clamp(_life+ value,0,_lifeLimit);
        Debug.Log($"�ω��O�F{last}�@�ω���F{_life}");
    }

    private void Start()
    {
        FieldManager.Instance.OnStart += ResetLife;
    }

    protected override void OnAwake()
    {
        //�V�[�����؂�ւ���Ă��l���ێ������悤��
        DontDestroyOnLoad(this);

        //���C�t��������
        _life = _initialLife;
    }

    void ResetLife()
    {
        _life = _initialLife;
    }

}
