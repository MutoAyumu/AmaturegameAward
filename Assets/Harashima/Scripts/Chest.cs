using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IAction
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
            PlayerStat.Instance.AddItem(_itemPrefabs[0]); //アイテム獲得
            _animator.SetTrigger("Open");
            isOpen = true; //開いたのでフラグをTrueに
        }        
    }
}

/// <summary>
/// フィールドオブジェクトの処理を実装するインターフェース
/// </summary>
public interface IAction
{
    void Action();
}
