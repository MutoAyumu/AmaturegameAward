using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    /// <summary> 獲得アイテムを格納するリスト</summary>
    List<GameObject> _inventry = new List<GameObject>();
    public List<GameObject> Inventry => _inventry;

    /// <summary>
    /// インベントリにアイテムを入れる関数
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        _inventry.Add(item);
        Debug.Log($"{item}を手に入れた");
    }

    public void RemoveItem(GameObject item)
    {
        _inventry.Remove(item);
    }

    protected override void OnAwake()
    {
        //シーンが切り替わっても値が保持されるように
        DontDestroyOnLoad(this);
    }
}
