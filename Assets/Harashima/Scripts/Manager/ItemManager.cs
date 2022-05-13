using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 二つ目の仕様のアイテムマネージャー
/// </summary>
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField, Tooltip("アイテムの数")]
    int _itemValue = 2;
    [SerializeField,Tooltip("アイテムの種類を保存した配列")]
    ItemBase[] _items;

    [SerializeField, Tooltip("一つのアイテムを持てる上限")]
    int _itemLimit = 99;

    /// <summary>アイテムをいくつ持っているかの配列</summary>
    int[] _inventry ;
    bool[] _first ;

    [SerializeField, Tooltip("アイテムを使う時のSE")]
    AudioClip[] _audios;

    /// <summary>
    /// アイテムの数を変化させる関数
    /// </summary>
    /// <param name="index">追加するアイテムの添え字</param>
    /// <param name="value">追加する数</param>
    public void ItemValueChange(int index,int value)
    {
        //最初はアイテムを生成
        if(!_first[index])
        {
            _first[index] = true;
            FieldManager.Instance.FirstGet(index);
        }
        //0以上、上限以下の数に収める
        _inventry[index] = Mathf.Clamp(_inventry[index]+value,0,_itemLimit);

        //UI上のTextも変更
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
        //UI上のテキストを0で初期化する
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
