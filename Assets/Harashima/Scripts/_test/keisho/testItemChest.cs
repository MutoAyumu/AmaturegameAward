using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testItemChest : testChestBase
{
    [SerializeField, Tooltip("�h���b�v����A�C�e��")]
    GameObject[] _itemPrefabs;

    protected override void OnAction()
    {
        testPlayerStat.Instance.AddItem(_itemPrefabs[0]); //�A�C�e���l��
        _animator.SetTrigger("Open");
    }
}
