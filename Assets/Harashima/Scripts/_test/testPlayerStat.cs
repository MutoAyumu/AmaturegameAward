using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仮のプレイヤーステータスクラス
/// </summary>
public class testPlayerStat : Singleton<testPlayerStat>
{
    /// <summary> 獲得アイテムを格納するリスト</summary>
    List<GameObject> _inventry = new List<GameObject>();
    public List<GameObject> Inventry => _inventry;

    [SerializeField,Tooltip("初期HP（現在は最大HPと兼用）")]
    int _initialHP = 10;

    /// <summary>プレイヤーの現在のHP</summary>
    int _hp;

    void Start()
    {
        _hp = _initialHP;
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

    /// <summary>
    /// HPを変化させる関数
    /// </summary>
    /// <param name="value">変化する値、正の値</param>
    public void HPfluctuation(int value)
    {
        if(_hp +value >= _initialHP)
        {
            Debug.Log($"HPは満タンです。現在のHP：{_hp}");
            return;
        }
        else
        {
            Debug.Log($"現在のHP：{_hp}、変更後のHP：{_hp+value}");
            _hp += value;           
        }        
    }
}
