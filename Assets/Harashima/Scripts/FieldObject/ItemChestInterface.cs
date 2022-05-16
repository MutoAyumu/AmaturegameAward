using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChestInterface : MonoBehaviour,IActivate,ISetText
{
    [SerializeField,Tooltip("ドロップするアイテム")]
    GameObject[] _itemPrefabs;

    [SerializeField, Tooltip("このオブジェクトのアニメーターコンポーネント")]
    Animator _animator;

    [SerializeField, Tooltip("アイテムを使う時のSE")]
    AudioClip[] _audios;

    [SerializeField] string _text = "B 宝箱を開ける";

    /// <summary>この宝箱が既に開いているかを判定するフラグ</summary>
    bool isOpen = false;

    public void Action()
    {
        if (!isOpen)
        {
            TestItemManager.Instance.AddItem(_itemPrefabs[0]); //アイテム獲得
            _animator.SetTrigger("Open");
            isOpen = true; //開いたのでフラグをTrueに
        }        
    }
    public string SetText()
    {
        return _text;
    }
}

/// <summary>
/// フィールドオブジェクトの処理を実装するインターフェース
/// </summary>
public interface IActivate
{
    void Action();
}
public interface ISetText
{ 
    string SetText();
}
