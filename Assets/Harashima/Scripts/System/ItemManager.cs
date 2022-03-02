using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    /// <summary> 獲得アイテムを格納するリスト</summary>
    GameObject[] _inventry ;
    public GameObject[] Inventry => _inventry;

    GameObject[] _UIinventry = new GameObject[4];
    public GameObject[] UIInventry => _UIinventry;

    [SerializeField, Tooltip("インベントリの上限値、基本は４")]
    const int _inventryLimit = 4;

    GameObject lastTryAddItem;
    public GameObject LastItem => lastTryAddItem;
    /// <summary>
    /// インベントリにアイテムを入れる関数
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        lastTryAddItem = item;
        for(int i = 0;i< _inventry.Length;i++)
        {
            if(!_inventry[i])
            {
                //UI上のインベントリに生成
                _UIinventry[i] =  Instantiate(item,FieldManager.Instance.InventryPanels[i].transform);
                _inventry[i] = item;
                Debug.Log($"{item}を手に入れた");
                return;
            }
        }
        //インベントリがいっぱいのとき
        FieldManager.Instance.ChoiceActive(true);
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < _inventry.Length; i++)
        {
            if (_inventry[i] == item)
            {
                Destroy(_UIinventry[i]); 
                _inventry[i] = null;
                return;
            }
        }
    }

    protected override void OnAwake()
    {
        //シーンが切り替わっても値が保持されるように
        DontDestroyOnLoad(this);

        _inventry = new GameObject[_inventryLimit];
    }

    public void InstanceItem()
    {
        for (int i = 0;i<Inventry.Length;i++)
        {
            if (_inventry[i])
            {
                _UIinventry[i] = Instantiate(_inventry[i], FieldManager.Instance.InventryPanels[i].transform);
            }
        }
    }
}
