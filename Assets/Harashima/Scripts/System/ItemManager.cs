using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    /// <summary> �l���A�C�e�����i�[���郊�X�g</summary>
    List<GameObject> _inventry = new List<GameObject>();
    public List<GameObject> Inventry => _inventry;

    /// <summary>
    /// �C���x���g���ɃA�C�e��������֐�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        _inventry.Add(item);
        Debug.Log($"{item}����ɓ��ꂽ");
    }

    public void RemoveItem(GameObject item)
    {
        _inventry.Remove(item);
    }

    protected override void OnAwake()
    {
        //�V�[�����؂�ւ���Ă��l���ێ������悤��
        DontDestroyOnLoad(this);
    }
}
