using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IActivate
{
    [SerializeField, Tooltip("ドロップするアイテム")]
    int _dropItemIndex;

    [SerializeField, Tooltip("ドロップする数")]
    int _dropValue  =1;

    [SerializeField, Tooltip("このオブジェクトのアニメーターコンポーネント")]
    Animator _animator;

    /// <summary>この宝箱が既に開いているかを判定するフラグ</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            ItemManager.Instance.ItemValueChange(_dropItemIndex, _dropValue);//アイテム獲得
            _animator.SetTrigger("Open");
            isOpen = true; //開いたのでフラグをTrueに
        }
    }
}
