using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ڂ̎d�l�̃A�C�e���}�l�[�W���[
/// </summary>
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField, Tooltip("�A�C�e���̐�")]
    int _itemValue = 2;
    [SerializeField,Tooltip("�A�C�e���̎�ނ�ۑ������z��")]
    ItemBase[] _items;

    [SerializeField, Tooltip("��̃A�C�e�������Ă���")]
    int _itemLimit = 99;

    /// <summary>�A�C�e�������������Ă��邩�̔z��</summary>
    int[] _inventry ;
    bool[] _first ;

    [SerializeField, Tooltip("�A�C�e�����g������SE")]
    AudioClip[] _audios;

    /// <summary>
    /// �A�C�e���̐���ω�������֐�
    /// </summary>
    /// <param name="index">�ǉ�����A�C�e���̓Y����</param>
    /// <param name="value">�ǉ����鐔</param>
    public void ItemValueChange(int index,int value)
    {
        //�ŏ��̓A�C�e���𐶐�
        if(!_first[index])
        {
            _first[index] = true;
            FieldManager.Instance.FirstGet(index);
        }
        //0�ȏ�A����ȉ��̐��Ɏ��߂�
        _inventry[index] = Mathf.Clamp(_inventry[index]+value,0,_itemLimit);

        //UI���Text���ύX
        FieldManager.Instance.ChangeTextValue(index, _inventry[index]);
    }

    public void UseItem(int index)
    {
        index = Mathf.Clamp(index, 0, _inventry.Length-1);
        if (_inventry[index]>0)
        {
            _items[index].Use();
            if (_audios.Length != 0)
            {
                SoundManager.Instance.SoundPlay(_audios[0]);
            }            
        }        
    }

    private void Start()
    {
        //UI��̃e�L�X�g��0�ŏ���������
        //for (int i = 0; i < 4; i++)
        //{
        //    FieldManager.Instance.ChangeTextValue(i, 0);
        //}
        _inventry = new int[_itemValue];
        _first = new bool[_itemValue];
    }
    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }
}
