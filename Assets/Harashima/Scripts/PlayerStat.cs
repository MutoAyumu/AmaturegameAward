using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Singleton<PlayerStat>
{
    /// <summary>
    /// 獲得アイテムを格納するリスト
    /// </summary>
    List<GameObject> _inventry = new List<GameObject>();
    public List<GameObject> Inventry => _inventry;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// インベントリにアイテムを入れる関数
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        _inventry.Add(item);
        Debug.Log($"{item}を手に入れた");
    }
}
