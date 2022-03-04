using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField,Tooltip("�A�C�e���̎�ނ�ۑ������z��")]
    ItemBase[] _items = new ItemBase[4];

    /// <summary>�A�C�e�������������Ă��邩�̔z��</summary>
    int[] _inventry = new int[4];

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }
}
