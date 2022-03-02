using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChestInterface : MonoBehaviour,IActivate
{
    [SerializeField,Tooltip("ドロップするアイテム")]
    GameObject[] _itemPrefabs;

    [SerializeField, Tooltip("このオブジェクトのアニメーターコンポーネント")]
    Animator _animator;

    /// <summary>この宝箱が既に開いているかを判定するフラグ</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            ItemManager.Instance.AddItem(_itemPrefabs[0]); //アイテム獲得
            _animator.SetTrigger("Open");
            isOpen = true; //開いたのでフラグをTrueに
        }        
    }
}

/// <summary>
/// フィールドオブジェクトの処理を実装するインターフェース
/// </summary>
public interface IActivate
{
    void Action();
}
