using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃v���C���[�X�e�[�^�X�N���X
/// </summary>
public class testPlayerStat : Singleton<testPlayerStat>
{
    /// <summary> �l���A�C�e�����i�[���郊�X�g</summary>
    List<GameObject> _inventry = new List<GameObject>();
    public List<GameObject> Inventry => _inventry;

    [SerializeField,Tooltip("����HP�i���݂͍ő�HP�ƌ��p�j")]
    int _initialHP = 10;

    /// <summary>�v���C���[�̌��݂�HP</summary>
    int _hp;

    void Start()
    {
        _hp = _initialHP;
    }


    /// <summary>
    /// �C���x���g���ɃA�C�e��������֐�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        _inventry.Add(item);
        Debug.Log($"{item}����ɓ��ꂽ");
    }

    /// <summary>
    /// HP��ω�������֐�
    /// </summary>
    /// <param name="value">�ω�����l�A���̒l</param>
    public void HPfluctuation(int value)
    {
        if(_hp +value >= _initialHP)
        {
            Debug.Log($"HP�͖��^���ł��B���݂�HP�F{_hp}");
            return;
        }
        else
        {
            Debug.Log($"���݂�HP�F{_hp}�A�ύX���HP�F{_hp+value}");
            _hp += value;           
        }        
    }
}
