using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField,Tooltip("アイテムの種類を保存した配列")]
    ItemBase[] _items = new ItemBase[4];

    /// <summary>アイテムをいくつ持っているかの配列</summary>
    int[] _inventry = new int[4];

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }
}
