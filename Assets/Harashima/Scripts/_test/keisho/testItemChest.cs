using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testItemChest : testChestBase
{
    [SerializeField, Tooltip("ドロップするアイテム")]
    GameObject[] _itemPrefabs;

    protected override void OnAction()
    {
        testPlayerStat.Instance.AddItem(_itemPrefabs[0]); //アイテム獲得
        _animator.SetTrigger("Open");
    }
}
